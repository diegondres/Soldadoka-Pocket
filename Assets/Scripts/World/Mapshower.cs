using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Clase para almacenar información de cada región
[System.Serializable]
public class Region
{
    public string nombre = "Región Sin Nombre";
    public int poblacion = 0;
    public float recursos = 0f;
    public Color32 colorOriginal; // El color original de la región en el mapa
    
    // Puedes añadir cualquier propiedad que necesites
    public string descripcion = "";
    
    // Constructor
    public Region(Color32 colorOriginal)
    {
        this.colorOriginal = colorOriginal;
        // Valores de ejemplo aleatorios (puedes inicializarlos como quieras)
        this.nombre = "Región " + Random.Range(1, 100);
        this.poblacion = Random.Range(1000, 1000000);
        this.recursos = Random.Range(10f, 100f);
    }
}

public class Mapshower : MonoBehaviour
{
    [Header("Configuración de Selección")]
    [Tooltip("Color que se aplicará a la región cuando se haga clic")]
    public Color32 regionSelectedColor = new Color32(255, 128, 0, 255); // Naranja por defecto
    
    // Mostrar información de la región en el Inspector para depuración
    [Header("Información de Región Actual")]
    [ReadOnly] public string regionNombre;
    [ReadOnly] public int regionPoblacion;
    [ReadOnly] public float regionRecursos;
    
    int width;
    int height;

    Color32[] remapArr;
    Texture2D paletteTex;

    Color32 prevColor;
    bool selectAny = false;

    // Variables para rastrear la selección actual
    private Color32 selectedRegionColor;
    private bool hasSelectedRegion = false;
    
    // Diccionario para mapear colores remapeados a instancias de Region
    private Dictionary<Color32, Region> regionesPorColor = new Dictionary<Color32, Region>();
    
    // Región actualmente seleccionada
    private Region regionSeleccionada = null;

    void Start()
    {
        var material = GetComponent<Renderer>().material;
        var mainTex = material.GetTexture("_MainTex") as Texture2D;
        var mainArr = mainTex.GetPixels32();

        width = mainTex.width;
        height = mainTex.height;

        var main2remap = new Dictionary<Color32, Color32>();
        remapArr = new Color32[mainArr.Length];
        int idx = 0;
        
        // Crear regiones para cada color único en el mapa
        for (int i = 0; i < mainArr.Length; i++)
        {
            var mainColor = mainArr[i];
            if (!main2remap.ContainsKey(mainColor))
            {
                var low = (byte)(idx % 256);
                var high = (byte)(idx / 256);
                Color32 remapsColor = new Color32(low, high, 0, 255);
                main2remap[mainColor] = remapsColor;
                
                // Crear nueva instancia de Region para este color
                Region nuevaRegion = new Region(mainColor);
                regionesPorColor[remapsColor] = nuevaRegion;
                
                idx++;
            }
            var remapColor = main2remap[mainColor];
            remapArr[i] = remapColor;
        }

        // Resto del código de inicialización sin cambios
        var paletteArr = new Color32[256 * 256];
        for (int i = 0; i < paletteArr.Length; i++)
        {
            paletteArr[i] = new Color32(255, 255, 255, 255);
        }

        var remapTex = new Texture2D(width, height, TextureFormat.RGBA32, false);
        remapTex.filterMode = FilterMode.Point;
        remapTex.SetPixels32(remapArr);
        remapTex.Apply(false);
        material.SetTexture("_RemapTex", remapTex);

        paletteTex = new Texture2D(256, 256, TextureFormat.RGBA32, false);
        paletteTex.filterMode = FilterMode.Point;
        paletteTex.SetPixels32(paletteArr);
        paletteTex.Apply(false);
        material.SetTexture("_PaletteTex", paletteTex);
        
        Debug.Log("Se han creado " + regionesPorColor.Count + " regiones únicas");
    }

    void Update()
    {
        var mousePos = Input.mousePosition;
        var ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hitInfo;
        if(Physics.Raycast(ray, out hitInfo)){
            var p = hitInfo.point;
            int x = (int)Mathf.Floor(p.x) + width / 2;
            int y = (int)Mathf.Floor(p.y) + height / 2;

            var remapColor = remapArr[x + y * width];

            // Detectar clic del mouse para seleccionar una región
            if(Input.GetMouseButtonDown(0)) {
                // Si ya hay una región seleccionada, restaurarla primero
                if(hasSelectedRegion) {
                    ChangeColor(selectedRegionColor, new Color32(255, 255, 255, 255));
                }
                
                // Si se vuelve a hacer clic en la misma región, deseleccionar
                if(hasSelectedRegion && selectedRegionColor.Equals(remapColor)) {
                    hasSelectedRegion = false;
                    regionSeleccionada = null;
                    ActualizarInformacionRegion(null);
                } else {
                    // Seleccionar nueva región
                    selectedRegionColor = remapColor;
                    hasSelectedRegion = true;
                    ChangeColor(selectedRegionColor, regionSelectedColor);
                    
                    // Obtener y almacenar la referencia a la región
                    if (regionesPorColor.TryGetValue(remapColor, out Region region)) {
                        regionSeleccionada = region;
                        ActualizarInformacionRegion(region);
                    }
                }
                
                paletteTex.Apply(false);
            }

            // Código existente para el efecto de hover
            if(!selectAny || !prevColor.Equals(remapColor)){
                if(selectAny){
                    // Solo restaurar el color anterior si no es la región seleccionada
                    if(!hasSelectedRegion || !prevColor.Equals(selectedRegionColor)) {
                        ChangeColor(prevColor, new Color32(255, 255, 255, 255));
                    } else if(hasSelectedRegion && prevColor.Equals(selectedRegionColor)) {
                        // Si es la región seleccionada, restaurar al color de selección
                        ChangeColor(prevColor, regionSelectedColor);
                    }
                }
                selectAny = true;
                prevColor = remapColor;
                
                // No resaltar con el efecto hover si es la región seleccionada
                if(!hasSelectedRegion || !remapColor.Equals(selectedRegionColor)) {
                    ChangeColor(remapColor, new Color32(50, 0, 255, 255));
                }
                
                paletteTex.Apply(false);
            }
        }
    }

    private void ChangeColor(Color32 remapColor, Color32 showColor)
    {
        int xp = remapColor[0];
        int yp = remapColor[1];

        paletteTex.SetPixel(xp, yp, showColor);
    }
    
    // Actualiza la información de la región para mostrarla en el Inspector
    private void ActualizarInformacionRegion(Region region)
    {
        if (region != null) {
            regionNombre = region.nombre;
            regionPoblacion = region.poblacion;
            regionRecursos = region.recursos;
            
            // Aquí podrías actualizar UI, disparar eventos, etc.
            Debug.Log("Región seleccionada: " + region.nombre + 
                      " | Población: " + region.poblacion + 
                      " | Recursos: " + region.recursos);
        } else {
            regionNombre = "Ninguna";
            regionPoblacion = 0;
            regionRecursos = 0;
        }
    }
    
    // Método público para acceder a la región seleccionada desde otros scripts
    public Region ObtenerRegionSeleccionada()
    {
        return regionSeleccionada;
    }
}

// Atributo para mostrar variables de solo lectura en el Inspector
public class ReadOnlyAttribute : PropertyAttribute {}

#if UNITY_EDITOR
[UnityEditor.CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : UnityEditor.PropertyDrawer
{
    public override void OnGUI(Rect position, UnityEditor.SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false;
        UnityEditor.EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }
}
#endif