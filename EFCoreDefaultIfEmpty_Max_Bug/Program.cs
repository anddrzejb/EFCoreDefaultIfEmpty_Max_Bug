﻿using System;
using System.Linq;

namespace EFCoreDefaultIfEmpty_Max_Bug
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new Ext.Context();
            int catId = 2;
            var catName = context.Categories.Where(c => c.Id == catId).Select(c => c.Name).SingleOrDefault();
            var valueOk = context.Products.Where(t => t.CategoryId == catId).Select(t => t.Version).DefaultIfEmpty().ToList().Max();            
            Console.WriteLine($"Value is {valueOk} for {catName}");

            var valueFail = context.Products.Where(t => t.CategoryId == catId).Select(t => t.Version).DefaultIfEmpty().Max();
            Console.WriteLine($"Value is {valueFail} for {catName}");
        }
    }
}
