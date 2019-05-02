using IxMilia.Step.Syntax;
using System;
using System.Collections.Generic;
using System.Text;

namespace IxMilia.Step.Items
{
    public class StepClosedShell : StepConnectedFaceSet
    {
        protected StepClosedShell( string name, IEnumerable<StepFace> cfsFaces ) : base( name, cfsFaces )
        {
        }

        public override StepItemType ItemType => StepItemType.ClosedShell;

        internal static StepClosedShell CreateFromSyntaxList( StepBinder binder, StepSyntaxList syntaxList )
        {
            syntaxList.AssertListCount( 2 );
            var faceSet = syntaxList.Values[1].GetValueList();
            var shell = new StepClosedShell( syntaxList.Values[0].GetStringValue(),
                                             new StepFace[faceSet.Values.Count] );

            for (int i = 0; i < faceSet.Values.Count; i++ )
            {
                var j = i; // capture to avoid rebinding
                binder.BindValue( faceSet.Values[j], v => shell.CfsFaces[j] = v.AsType<StepFace>() );
            }

            return shell;
        }
    }
}
