using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Lumpn
{
    public static class ProgressBarUtils
    {
        public sealed class Progress : System.IDisposable
        {
            private readonly string title;
            private readonly int count;
            private int current;

            public Progress(string title, int count)
            {
                this.title = title;
                this.count = count;
            }

            public bool Update(string info)
            {
                return EditorUtility.DisplayCancelableProgressBar(title, info, (float)current++ / count);
            }

            public void Dispose()
            {
                EditorUtility.ClearProgressBar();
            }
        }

        public static Progress Create(string title, int count)
        {
            return new Progress(title, count);
        }
    }
}
