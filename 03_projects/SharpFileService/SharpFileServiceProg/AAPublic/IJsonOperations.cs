namespace SharpFileServiceProg.AAPublic
{
    public interface IJsonOperations
    {
        T DeserializeObject<T>(string jsonString);
        T TryDeserializeObject<T>(string jsonString);
        string SerializeObject<T>(object obj);
        string TrySerializeObject<T>(object obj);
    }
}