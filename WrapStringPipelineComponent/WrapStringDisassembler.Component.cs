namespace BizTalkComponents.PipelineComponents.WrapStringPipelineComponent.PipelineComponents
{
    using System;
    using System.Resources;
    using System.Drawing;
    using System.Collections;
    using System.Reflection;
    using System.ComponentModel;
    using System.Text;
    using System.IO;
    using Microsoft.BizTalk.Message.Interop;
    using Microsoft.BizTalk.Component.Interop;
    using System.Collections.Generic;
    using BizTalkComponents.Utils;

    [ComponentCategory(CategoryTypes.CATID_PipelineComponent)]
    [ComponentCategory(CategoryTypes.CATID_Any)]
    [ComponentCategory(CategoryTypes.CATID_Validate)]
    [System.Runtime.InteropServices.Guid("4971A7E3-F467-433F-9D6D-688234EB6F4D")]
    public partial class WrapStringDisassembler : IBaseComponent, IPersistPropertyBag, IComponentUI
    {
        #region IBaseComponent

        /// <summary>
        /// Name of the component.
        /// </summary>
        [Browsable(false)]
        public string Name
        {
            get { return "WrapStringDisassembler"; }
        }

        /// <summary>
        /// Version of the component.
        /// </summary>
        [Browsable(false)]
        public string Version
        {
            get { return "1.0"; }
        }

        /// <summary>
        /// Description of the component.
        /// </summary>
        [Browsable(false)]
        public string Description
        {
            get { return "Some description"; }
        }

        #endregion

        #region IPersistPropertyBag

        /// <summary>
        /// Gets class ID of component for usage from unmanaged code.
        /// </summary>
        /// <param name="classid">Class ID of the component.</param>
        public void GetClassID(out Guid classid)
        {
            classid = new System.Guid("4971A7E3-F467-433F-9D6D-688234EB6F4D");
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        public void InitNew()
        {
        }

        /// <summary>
        /// Loads configuration property for component.
        /// </summary>
        /// <param name="pb">Configuration property bag.</param>
        /// <param name="errlog">Error status (not used in this code).</param>
        public void Load(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, Int32 errlog)
        {
            EncodingName = PropertyBagHelper.ReadPropertyBag(pb, "Encoding", "utf-8");
        }

        /// <summary>
        /// Saves the current component configuration into the property bag.
        /// </summary>
        /// <param name="pb">Configuration property bag.</param>
        /// <param name="fClearDirty">Not used.</param>
        /// <param name="fSaveAllProperties">Not used.</param>
        public void Save(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, Boolean fClearDirty, Boolean fSaveAllProperties)
        {
            PropertyBagHelper.WritePropertyBag(pb, "Encoding", EncodingName);
        }

        #endregion

        #region IComponentUI

        /// <summary>
        /// Component icon to use in BizTalk Editor.
        /// </summary>
        [Browsable(false)]
        public IntPtr Icon
        {
            get
            {
                return IntPtr.Zero;
            }

        }

        /// <summary>
        /// The Validate method is called by the BizTalk Editor during the build 
        /// of a BizTalk project.
        /// </summary>
        /// <param name="obj">Project system.</param>
        /// <returns>
        /// A list of error and/or warning messages encounter during validation
        /// of this component.
        /// </returns>
        public IEnumerator Validate(object obj)
        {
            IEnumerator enumerator = null;
            return enumerator;
        }

        #endregion
    }
}

