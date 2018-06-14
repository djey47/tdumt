using TDUModdingLibrary.support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DjeFramework1.Util.BasicStructures;
using System.Collections.Generic;

namespace TDUModdingLibraryTests
{
    /// <summary>
    ///Classe de test pour ToolsTest, destinée à contenir tous
    ///les tests unitaires ToolsTest
    ///</summary>
    [TestClass()]
    public class ToolsTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Obtient ou définit le contexte de test qui fournit
        ///des informations sur la série de tests active ainsi que ses fonctionnalités.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Attributs de tests supplémentaires
        // 
        //Vous pouvez utiliser les attributs supplémentaires suivants lors de l'écriture de vos tests :
        //
        //Utilisez ClassInitialize pour exécuter du code avant d'exécuter le premier test dans la classe
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Utilisez ClassCleanup pour exécuter du code après que tous les tests ont été exécutés dans une classe
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Utilisez TestInitialize pour exécuter du code avant d'exécuter chaque test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Utilisez TestCleanup pour exécuter du code après que chaque test a été exécuté
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        /// <summary>
        ///Test pour ParseCouples
        ///</summary>
        [TestMethod()]
        public void ParseCouples()
        {
            //Given
            string values = "123|Test||456|Test2||";
            Couple<string> expectedCouple1 = new Couple<string>("123","Test");
            Couple<string> expectedCouple2 = new Couple<string>("456", "Test2");

            //When
            List<Couple<string>> actualCouples = Tools.ParseCouples(values);

            //Then
            Assert.AreEqual(2, actualCouples.Count);
            Assert.AreEqual(expectedCouple1, actualCouples[0]);
            Assert.AreEqual(expectedCouple2, actualCouples[1]);
        }
    }
}