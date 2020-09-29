using NUnit.Framework;
using UnityEditor;

namespace Rara.Tests
{
    public sealed class ShaderCompilerTest
    {
        [Test]
        public void TestShaderHasErrors()
        {
            var infos = ShaderUtil.GetAllShaderInfo();
            foreach (var info in infos)
            {
                Assert.IsFalse(info.hasErrors, "Shader '{0}' has errors.", info.name);
            }
        }
    }
}
