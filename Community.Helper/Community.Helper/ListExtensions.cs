using System.Collections.Generic;
using System.Linq;

namespace Community.Helper
{
    public static class ListExtensions
    {
        public static void RemoveRange<T>(this List<T> source, IEnumerable<T> rangeToRemove)
        {
            //TODO: Paso 10 - 3 - Seleccionar Campos individuales - Se elimina una coleccion a partir de otra
            if (rangeToRemove == null | !rangeToRemove.Any())
                return;

            foreach (T item in rangeToRemove)
            {
                source.Remove(item);
            }


        }
    }
}
