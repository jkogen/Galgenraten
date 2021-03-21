using Galgenraten.Core.Models;
using Galgenraten.Core.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Galgenraten.Test
{
    [TestClass]
    public class ViewModelTests
    {
        [TestMethod]
        public void TesteMainViewModel()
        {
            var vm = new MainViewModel();

            var buchstaben = "abcdefghijklmnopqrstuvwxyzöäü,.-+'~";

            foreach (var buchstabe in buchstaben.Select(a => a))
                vm.RateBuchstabenExecute(buchstabe);

            vm.StarteNeuesSpielExecute(null);
            vm.AufloesenExecute(null);
        }
    }
}
