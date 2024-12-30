using UnityEngine;

public class InputHandler : MonoBehaviour {
    void Update(){
        if(Battleground.Instance.advanceButton.activeSelf){
            if(Input.GetKeyDown(KeyCode.Space)){
                Battleground.Instance.Advance();
            }
        }
    }
}