using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestFormatting
    {
        [Test]
        public void TestRefresh()
        {
            AssetDatabase.Refresh();
        }

        [Test]
        public void TestLineEndings()
        {
            var guids = AssetDatabase.FindAssets("t:script");
            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
            }
        }
    }
}
