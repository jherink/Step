//Copyright( c) IxMilia.All Rights Reserved.Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using IxMilia.Step.Syntax;
using System;
using System.Collections.Generic;

namespace IxMilia.Step.Items
{
    public class StepOpenShell : StepConnectedFaceSet
    {
        protected StepOpenShell( string name, IEnumerable<StepFace> cfsFaces ) : base( name, cfsFaces )
        {
        }

        public override StepItemType ItemType => StepItemType.OpenShell;

        internal static StepOpenShell CreateFromSyntaxList( StepBinder binder, StepSyntaxList syntaxList )
        {
            syntaxList.AssertListCount( 2 );
            var faceSet = syntaxList.Values[1].GetValueList();
            var shell = new StepOpenShell( syntaxList.Values[0].GetStringValue(),
                                           new StepFace[faceSet.Values.Count] );

            for ( int i = 0; i < faceSet.Values.Count; i++ )
            {
                var j = i; // capture to avoid rebinding
                binder.BindValue( faceSet.Values[j], v => shell.CfsFaces[j] = v.AsType<StepFace>() );
            }

            return shell;
        }
    }
}
