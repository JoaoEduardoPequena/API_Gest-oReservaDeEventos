namespace Infrastruture.Worker.Interfaces
{
    public interface IRedisService
    {
        public Task<T> GetAsync<T>(string key);
        public Task<T> SetAsync<T>(string key, T value, int expirationTime, string unit);
    }
}
