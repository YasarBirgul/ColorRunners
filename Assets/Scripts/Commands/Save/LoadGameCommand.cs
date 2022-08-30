using Abstract;
using Datas.ValueObject;
using Enums;

namespace Commands.Save
{
    public class LoadGameCommand
    {
        public T Execute<T>(string key, int uniqueId)
        {
            string _path = key + uniqueId.ToString() + ".es3";
            
            if (ES3.FileExists(_path))
            {
                if (ES3.KeyExists(key,_path))
                {
                    T objectToReturn = ES3.Load<T>(key,_path);
                    
                    return objectToReturn;
                }
                else
                {
                    return default(T); 
                }
            }
            return default(T);
        }
    }
}