namespace Prototype.GameInterface
{
    public interface IMapEntityLayer
    {
        public void AddEntity(MapEntityArgs args, out int entityId);

        public void UpdateEntity(int entityId, MapEntityArgs args);

        public void RemoveEntity(int entityId);
    }
}