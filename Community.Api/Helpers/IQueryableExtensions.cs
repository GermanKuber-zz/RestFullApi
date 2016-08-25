﻿using System;
using System.Linq;
using System.Linq.Dynamic;

namespace Community.APi.Helpers
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ApplySort<T>(this IQueryable<T> source, string sort)
        {
            //TODO: Paso 8 - 1 - Ordenamiento - Implementamos metodo de extension para realizar sorting sobre nuestros resultados
            //Instalamos el package  System.Linq.Dynamic
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (sort == null)
            {
                return source;
            }

            // split the sort string
            var lstSort = sort.Split(',');



            string completeSortExpression = "";
            foreach (var sortOption in lstSort)
            {
     

                if (sortOption.StartsWith("-"))
                {
                    completeSortExpression = completeSortExpression + sortOption.Remove(0, 1) + " descending,";
                }
                else
                {
                    completeSortExpression = completeSortExpression + sortOption + ",";
                }

            }

            if (!string.IsNullOrWhiteSpace(completeSortExpression))
            {
                source = source.OrderBy<T>(completeSortExpression.Remove(completeSortExpression.Count() - 1));
            }

            return source;
        }
    }
}