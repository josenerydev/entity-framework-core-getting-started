using Microsoft.EntityFrameworkCore;
using SamuraiApp.Data;
using SamuraiApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ConsoleApp
{
    internal class Program
    {
        private static SamuraiContext _context = new SamuraiContext();

        private static void Main(string[] args)
        {
            InsertMultipleSamurais();
        }

        private static void InsertMultipleSamurais()
        {
            //var samurai = new Samurai { Name = "Sampson" };
            //var samurai2 = new Samurai { Name = "Tasha" };
            //var samurai3 = new Samurai { Name = "Number3" };
            //var samurai4 = new Samurai { Name = "Number 4" };
            var _bizdata = new BusinessDataLogic();
            var samuraiNames = new string[] { "Sampson", "Tasha", "Number3", "Number4" };
            var newSamuraisCreated = _bizdata.AddMultipleSamurais(samuraiNames);
        }
    }
}
