using System.Collections.Generic;
using System;

namespace Samples.Entities.SampleActionEntities
{
    internal class SampleInstance
    {
        public Selection selection { get; private set; }

        public Copy copy { get; private set; }

        public SampleInstance()
        {
            this.selection = new Selection();
            this.copy = new Copy();
        }

        public void SampleRun(List<String> dbase, List<string> extensions, List<string> pathCopyFrom, List<string> pathCopyTo, int dbaseType, List<String> columns, List<String> finalColumns)
        {
            this.copy.CopyFiles(dbase, extensions, pathCopyFrom, pathCopyTo, dbaseType);
            this.selection.SelectionStart(pathCopyTo, extensions, columns, finalColumns, dbase);
            
        }
    }
}
