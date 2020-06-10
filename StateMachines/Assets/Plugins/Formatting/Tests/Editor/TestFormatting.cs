using System.IO;
using NUnit.Framework;
using UnityEditor;

namespace Lumpn
{
    [TestFixture]
    public sealed class TestFormatting
    {
        [Test]
        public void TestLineEndings()
        {
            RunTest("Test line endings", TestLineEndings);
        }

        [Test]
        public void TestTabsVersusSpaces()
        {
            RunTest("Test spaces vs. tabs", TestTabsVersusSpaces);
        }

        [Test]
        public void TestIndentation()
        {
            RunTest("Test indentation", TestIndentation);
        }

        [Test]
        public void TestTrailingWhitespace()
        {
            RunTest("Test trailing whitespaces", TestTrailingWhitespaces);
        }

        [Test]
        public void TestFinalNewLine()
        {
            RunTest("Test final new line", TestFinalNewLine);
        }

        [Test]
        public void TestPlainASCII()
        {
            RunTest("Test plain ASCII", TestPlainASCII);
        }

        public static void TestLineEndings(string path)
        {
            using (var file = File.OpenRead(path))
            {
                int lineNumber = 1;
                int value;
                while ((value = file.ReadByte()) >= 0)
                {
                    if (value == 10) { lineNumber++; }
                    Assert.AreNotEqual(13, value, "File '{0}' has Windows style line ending in line {1}", path, lineNumber);
                }
            }
        }

        public static void TestTabsVersusSpaces(string path)
        {
            using (var file = File.OpenRead(path))
            {
                int lineNumber = 1;
                int value;
                while ((value = file.ReadByte()) >= 0)
                {
                    if (value == 10) { lineNumber++; }
                    Assert.AreNotEqual(9, value, "File '{0}' has a tab character in line {1}", path, lineNumber);
                }
            }
        }

        public static void TestIndentation(string path)
        {
            using (var file = File.OpenRead(path))
            {
                int lineNumber = 1;
                int consecutiveSpaces = 0;
                bool counting = true;
                int value;
                while ((value = file.ReadByte()) >= 0)
                {
                    if (counting)
                    {
                        if (value == 32)
                        {
                            consecutiveSpaces++;
                        }
                        else
                        {
                            Assert.AreEqual((consecutiveSpaces / 4) * 4, consecutiveSpaces, "File '{0}' is not using four spaces for indentation in line {1}", path, lineNumber);
                            counting = false;
                        }
                    }
                    if (value == 10)
                    {
                        lineNumber++;
                        consecutiveSpaces = 0;
                        counting = true;
                    }
                }
            }
        }

        public static void TestTrailingWhitespaces(string path)
        {
            using (var file = File.OpenRead(path))
            {
                int lineNumber = 1;
                int consecutiveSpaces = 0;
                int value;
                while ((value = file.ReadByte()) >= 0)
                {
                    if (value == 10)
                    {
                        Assert.AreEqual(0, consecutiveSpaces, "File '{0}' has a trailing whitespaces in line {1}", path, lineNumber);
                        lineNumber++;
                    }
                    if (value == 32)
                    {
                        consecutiveSpaces++;
                    }
                    else
                    {
                        consecutiveSpaces = 0;
                    }
                }
            }
        }

        public static void TestFinalNewLine(string path)
        {
            using (var file = File.OpenRead(path))
            {
                int consecutiveLinefeeds = 0;
                int value;
                while ((value = file.ReadByte()) >= 0)
                {
                    if (value == 10)
                    {
                        consecutiveLinefeeds++;
                    }
                    else
                    {
                        consecutiveLinefeeds = 0;
                    }
                }
                Assert.AreEqual(1, consecutiveLinefeeds, "File '{0}' must have a final new line character at the end", path);
            }
        }

        public static void TestPlainASCII(string path)
        {
            using (var file = File.OpenRead(path))
            {
                int lineNumber = 1;
                int value;
                while ((value = file.ReadByte()) >= 0)
                {
                    if (value == 10) { lineNumber++; }
                    Assert.Less(value, 128, "File '{0}' has a non-ASCII character in line {1}", path, lineNumber);
                }
            }
        }

        private static void RunTest(string title, System.Action<string> test)
        {
            var guids = AssetDatabase.FindAssets("t:script t:shader");
            using (var pb = ProgressBarUtils.Create(title, guids.Length))
            {
                foreach (var guid in guids)
                {
                    var path = AssetDatabase.GUIDToAssetPath(guid);
                    if (pb.Update(path)) break;

                    if (FormattingUtils.IsNonScript(path)) continue;
                    test(path);
                }
            }
        }
    }
}
