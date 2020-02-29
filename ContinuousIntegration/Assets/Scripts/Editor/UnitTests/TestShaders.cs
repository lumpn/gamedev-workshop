using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestShaders
    {
        [Test]
        public void ShaderHasErrors()
        {
            var infos = ShaderUtil.GetAllShaderInfo();
            foreach (var info in infos)
            {
                Assert.IsFalse(info.hasErrors, "Shader '{0}' has errors.", info.name);
            }
        }
    }
}
