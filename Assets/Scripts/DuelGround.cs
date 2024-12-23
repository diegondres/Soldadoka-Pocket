public class DuelGround
{
    public string biome;
    public Unidad allied;
    public Unidad enemy;
    public void AddAllied(Unidad allied)
    {
        this.allied = allied;
    }
    public void AddEnemy(Unidad enemy)
    {
        this.enemy = enemy;
    }
    
    public class DuelGroundBuilder
    {
        private DuelGround ground;

        public DuelGroundBuilder()
        {
            ground = new DuelGround();
        }

        public DuelGroundBuilder withBiome(string biome)
        {
            ground.biome = biome;
            return this;
        }

        public DuelGroundBuilder withAllied(Unidad allied)
        {
            ground.allied = allied;
            return this;
        }

        public DuelGroundBuilder withEnemy(Unidad enemy)
        {
            ground.enemy = enemy;
            return this;
        }

        public DuelGround build()
        {
            return ground;
        }
    }
}
