using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;

namespace Tests
{
    [TestFixture]
    public sealed class TestAssetBundles
    {
        [Test]
        public void AssetDuplication()
        {
            // AssetDatabase.GetDependencies only works for saved assets
            AssetDatabase.SaveAssets();

            // build map of explicitly assigned assets
            var assigned = new Dictionary<string, string>();
            foreach (var bundleName in AssetDatabase.GetAllAssetBundleNames())
            {
                var assetPaths = AssetDatabase.GetAssetPathsFromAssetBundle(bundleName);
                foreach (var assetPath in assetPaths)
                {
                    assigned.Add(assetPath, bundleName);
                }
            }

            // build map of implicitly assigned assets
            var referenced = new Dictionary<string, string>();
            foreach (var bundleName in AssetDatabase.GetAllAssetBundleNames())
            {
                var assetPaths = AssetDatabase.GetAssetPathsFromAssetBundle(bundleName);
                var dependencies = AssetDatabase.GetDependencies(assetPaths, true);
                foreach (var assetPath in dependencies)
                {
                    if (assigned.ContainsKey(assetPath))
                    {
                        // dependency explicitly assigned to a bundle
                        continue;
                    }

                    string otherBundleName;
                    if (referenced.TryGetValue(assetPath, out otherBundleName))
                    {
                        // dependency implicitly assigned to another bundle -> asset duplication
                        Assert.AreEqual(otherBundleName, bundleName, "Asset '{0}' implicitly referenced by asset bundles '{1}' and '{2}'", assetPath, otherBundleName, bundleName);
                    }
                    else
                    {
                        referenced.Add(assetPath, bundleName);
                    }
                }
            }
        }
    }
}
