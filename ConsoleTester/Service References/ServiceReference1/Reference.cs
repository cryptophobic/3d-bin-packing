﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConsoleTester.ServiceReference1 {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="BoxesArgument", Namespace="http://tempuri.org/")]
    [System.SerializableAttribute()]
    public partial class BoxesArgument : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string prodidField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string nameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string heightField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string widthField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string depthField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string weightField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string prodid {
            get {
                return this.prodidField;
            }
            set {
                if ((object.ReferenceEquals(this.prodidField, value) != true)) {
                    this.prodidField = value;
                    this.RaisePropertyChanged("prodid");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string name {
            get {
                return this.nameField;
            }
            set {
                if ((object.ReferenceEquals(this.nameField, value) != true)) {
                    this.nameField = value;
                    this.RaisePropertyChanged("name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string height {
            get {
                return this.heightField;
            }
            set {
                if ((object.ReferenceEquals(this.heightField, value) != true)) {
                    this.heightField = value;
                    this.RaisePropertyChanged("height");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string width {
            get {
                return this.widthField;
            }
            set {
                if ((object.ReferenceEquals(this.widthField, value) != true)) {
                    this.widthField = value;
                    this.RaisePropertyChanged("width");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string depth {
            get {
                return this.depthField;
            }
            set {
                if ((object.ReferenceEquals(this.depthField, value) != true)) {
                    this.depthField = value;
                    this.RaisePropertyChanged("depth");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string weight {
            get {
                return this.weightField;
            }
            set {
                if ((object.ReferenceEquals(this.weightField, value) != true)) {
                    this.weightField = value;
                    this.RaisePropertyChanged("weight");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ContainersArgument", Namespace="http://tempuri.org/")]
    [System.SerializableAttribute()]
    public partial class ContainersArgument : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string heightField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string widthField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string depthField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string weightField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string height {
            get {
                return this.heightField;
            }
            set {
                if ((object.ReferenceEquals(this.heightField, value) != true)) {
                    this.heightField = value;
                    this.RaisePropertyChanged("height");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string width {
            get {
                return this.widthField;
            }
            set {
                if ((object.ReferenceEquals(this.widthField, value) != true)) {
                    this.widthField = value;
                    this.RaisePropertyChanged("width");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string depth {
            get {
                return this.depthField;
            }
            set {
                if ((object.ReferenceEquals(this.depthField, value) != true)) {
                    this.depthField = value;
                    this.RaisePropertyChanged("depth");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string weight {
            get {
                return this.weightField;
            }
            set {
                if ((object.ReferenceEquals(this.weightField, value) != true)) {
                    this.weightField = value;
                    this.RaisePropertyChanged("weight");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Solution", Namespace="http://tempuri.org/")]
    [System.SerializableAttribute()]
    public partial class Solution : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string containersField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string packedVolumeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string containersVolumeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string freeVolumeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private ConsoleTester.ServiceReference1.ArrayOfContainerParams removedContainersField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private ConsoleTester.ServiceReference1.ArrayOfProductParams removedBoxesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private ConsoleTester.ServiceReference1.ResultedContainers[] containerField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string containers {
            get {
                return this.containersField;
            }
            set {
                if ((object.ReferenceEquals(this.containersField, value) != true)) {
                    this.containersField = value;
                    this.RaisePropertyChanged("containers");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string packedVolume {
            get {
                return this.packedVolumeField;
            }
            set {
                if ((object.ReferenceEquals(this.packedVolumeField, value) != true)) {
                    this.packedVolumeField = value;
                    this.RaisePropertyChanged("packedVolume");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string containersVolume {
            get {
                return this.containersVolumeField;
            }
            set {
                if ((object.ReferenceEquals(this.containersVolumeField, value) != true)) {
                    this.containersVolumeField = value;
                    this.RaisePropertyChanged("containersVolume");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string freeVolume {
            get {
                return this.freeVolumeField;
            }
            set {
                if ((object.ReferenceEquals(this.freeVolumeField, value) != true)) {
                    this.freeVolumeField = value;
                    this.RaisePropertyChanged("freeVolume");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public ConsoleTester.ServiceReference1.ArrayOfContainerParams removedContainers {
            get {
                return this.removedContainersField;
            }
            set {
                if ((object.ReferenceEquals(this.removedContainersField, value) != true)) {
                    this.removedContainersField = value;
                    this.RaisePropertyChanged("removedContainers");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public ConsoleTester.ServiceReference1.ArrayOfProductParams removedBoxes {
            get {
                return this.removedBoxesField;
            }
            set {
                if ((object.ReferenceEquals(this.removedBoxesField, value) != true)) {
                    this.removedBoxesField = value;
                    this.RaisePropertyChanged("removedBoxes");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=6)]
        public ConsoleTester.ServiceReference1.ResultedContainers[] container {
            get {
                return this.containerField;
            }
            set {
                if ((object.ReferenceEquals(this.containerField, value) != true)) {
                    this.containerField = value;
                    this.RaisePropertyChanged("container");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.CollectionDataContractAttribute(Name="ArrayOfContainerParams", Namespace="http://tempuri.org/", ItemName="containerParams")]
    [System.SerializableAttribute()]
    public class ArrayOfContainerParams : System.Collections.Generic.List<ConsoleTester.ServiceReference1.containerParams> {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.CollectionDataContractAttribute(Name="ArrayOfProductParams", Namespace="http://tempuri.org/", ItemName="productParams")]
    [System.SerializableAttribute()]
    public class ArrayOfProductParams : System.Collections.Generic.List<ConsoleTester.ServiceReference1.productParams> {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ResultedContainers", Namespace="http://tempuri.org/")]
    [System.SerializableAttribute()]
    public partial class ResultedContainers : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private ConsoleTester.ServiceReference1.containerParams containerParamsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string packedBoxesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string packedVolumeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string freeVolumeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private ConsoleTester.ServiceReference1.ArrayOfPackedProducts productField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string No {
            get {
                return this.NoField;
            }
            set {
                if ((object.ReferenceEquals(this.NoField, value) != true)) {
                    this.NoField = value;
                    this.RaisePropertyChanged("No");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public ConsoleTester.ServiceReference1.containerParams containerParams {
            get {
                return this.containerParamsField;
            }
            set {
                if ((object.ReferenceEquals(this.containerParamsField, value) != true)) {
                    this.containerParamsField = value;
                    this.RaisePropertyChanged("containerParams");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string packedBoxes {
            get {
                return this.packedBoxesField;
            }
            set {
                if ((object.ReferenceEquals(this.packedBoxesField, value) != true)) {
                    this.packedBoxesField = value;
                    this.RaisePropertyChanged("packedBoxes");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string packedVolume {
            get {
                return this.packedVolumeField;
            }
            set {
                if ((object.ReferenceEquals(this.packedVolumeField, value) != true)) {
                    this.packedVolumeField = value;
                    this.RaisePropertyChanged("packedVolume");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string freeVolume {
            get {
                return this.freeVolumeField;
            }
            set {
                if ((object.ReferenceEquals(this.freeVolumeField, value) != true)) {
                    this.freeVolumeField = value;
                    this.RaisePropertyChanged("freeVolume");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public ConsoleTester.ServiceReference1.ArrayOfPackedProducts product {
            get {
                return this.productField;
            }
            set {
                if ((object.ReferenceEquals(this.productField, value) != true)) {
                    this.productField = value;
                    this.RaisePropertyChanged("product");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="containerParams", Namespace="http://tempuri.org/")]
    [System.SerializableAttribute()]
    public partial class containerParams : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string heightField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string widthField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string depthField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string volumeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string weightField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string height {
            get {
                return this.heightField;
            }
            set {
                if ((object.ReferenceEquals(this.heightField, value) != true)) {
                    this.heightField = value;
                    this.RaisePropertyChanged("height");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string width {
            get {
                return this.widthField;
            }
            set {
                if ((object.ReferenceEquals(this.widthField, value) != true)) {
                    this.widthField = value;
                    this.RaisePropertyChanged("width");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string depth {
            get {
                return this.depthField;
            }
            set {
                if ((object.ReferenceEquals(this.depthField, value) != true)) {
                    this.depthField = value;
                    this.RaisePropertyChanged("depth");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string volume {
            get {
                return this.volumeField;
            }
            set {
                if ((object.ReferenceEquals(this.volumeField, value) != true)) {
                    this.volumeField = value;
                    this.RaisePropertyChanged("volume");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string weight {
            get {
                return this.weightField;
            }
            set {
                if ((object.ReferenceEquals(this.weightField, value) != true)) {
                    this.weightField = value;
                    this.RaisePropertyChanged("weight");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="productParams", Namespace="http://tempuri.org/")]
    [System.SerializableAttribute()]
    public partial class productParams : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string prodidField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string nameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string heightField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string widthField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string depthField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string weightField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string prodid {
            get {
                return this.prodidField;
            }
            set {
                if ((object.ReferenceEquals(this.prodidField, value) != true)) {
                    this.prodidField = value;
                    this.RaisePropertyChanged("prodid");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string name {
            get {
                return this.nameField;
            }
            set {
                if ((object.ReferenceEquals(this.nameField, value) != true)) {
                    this.nameField = value;
                    this.RaisePropertyChanged("name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string height {
            get {
                return this.heightField;
            }
            set {
                if ((object.ReferenceEquals(this.heightField, value) != true)) {
                    this.heightField = value;
                    this.RaisePropertyChanged("height");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string width {
            get {
                return this.widthField;
            }
            set {
                if ((object.ReferenceEquals(this.widthField, value) != true)) {
                    this.widthField = value;
                    this.RaisePropertyChanged("width");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string depth {
            get {
                return this.depthField;
            }
            set {
                if ((object.ReferenceEquals(this.depthField, value) != true)) {
                    this.depthField = value;
                    this.RaisePropertyChanged("depth");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string weight {
            get {
                return this.weightField;
            }
            set {
                if ((object.ReferenceEquals(this.weightField, value) != true)) {
                    this.weightField = value;
                    this.RaisePropertyChanged("weight");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.CollectionDataContractAttribute(Name="ArrayOfPackedProducts", Namespace="http://tempuri.org/", ItemName="packedProducts")]
    [System.SerializableAttribute()]
    public class ArrayOfPackedProducts : System.Collections.Generic.List<ConsoleTester.ServiceReference1.packedProducts> {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="packedProducts", Namespace="http://tempuri.org/")]
    [System.SerializableAttribute()]
    public partial class packedProducts : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private ConsoleTester.ServiceReference1.productParams productParamsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string xField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string yField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string zField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public ConsoleTester.ServiceReference1.productParams productParams {
            get {
                return this.productParamsField;
            }
            set {
                if ((object.ReferenceEquals(this.productParamsField, value) != true)) {
                    this.productParamsField = value;
                    this.RaisePropertyChanged("productParams");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string x {
            get {
                return this.xField;
            }
            set {
                if ((object.ReferenceEquals(this.xField, value) != true)) {
                    this.xField = value;
                    this.RaisePropertyChanged("x");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string y {
            get {
                return this.yField;
            }
            set {
                if ((object.ReferenceEquals(this.yField, value) != true)) {
                    this.yField = value;
                    this.RaisePropertyChanged("y");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string z {
            get {
                return this.zField;
            }
            set {
                if ((object.ReferenceEquals(this.zField, value) != true)) {
                    this.zField = value;
                    this.RaisePropertyChanged("z");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.ServiceSoap")]
    public interface ServiceSoap {
        
        // CODEGEN: Generating message contract since element name boxesString from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/calculate", ReplyAction="*")]
        ConsoleTester.ServiceReference1.calculateResponse calculate(ConsoleTester.ServiceReference1.calculateRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class calculateRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="calculate", Namespace="http://tempuri.org/", Order=0)]
        public ConsoleTester.ServiceReference1.calculateRequestBody Body;
        
        public calculateRequest() {
        }
        
        public calculateRequest(ConsoleTester.ServiceReference1.calculateRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class calculateRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public ConsoleTester.ServiceReference1.BoxesArgument[] boxesString;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public ConsoleTester.ServiceReference1.ContainersArgument[] containersString;
        
        public calculateRequestBody() {
        }
        
        public calculateRequestBody(ConsoleTester.ServiceReference1.BoxesArgument[] boxesString, ConsoleTester.ServiceReference1.ContainersArgument[] containersString) {
            this.boxesString = boxesString;
            this.containersString = containersString;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class calculateResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="calculateResponse", Namespace="http://tempuri.org/", Order=0)]
        public ConsoleTester.ServiceReference1.calculateResponseBody Body;
        
        public calculateResponse() {
        }
        
        public calculateResponse(ConsoleTester.ServiceReference1.calculateResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class calculateResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public ConsoleTester.ServiceReference1.Solution calculateResult;
        
        public calculateResponseBody() {
        }
        
        public calculateResponseBody(ConsoleTester.ServiceReference1.Solution calculateResult) {
            this.calculateResult = calculateResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ServiceSoapChannel : ConsoleTester.ServiceReference1.ServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceSoapClient : System.ServiceModel.ClientBase<ConsoleTester.ServiceReference1.ServiceSoap>, ConsoleTester.ServiceReference1.ServiceSoap {
        
        public ServiceSoapClient() {
        }
        
        public ServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ConsoleTester.ServiceReference1.calculateResponse ConsoleTester.ServiceReference1.ServiceSoap.calculate(ConsoleTester.ServiceReference1.calculateRequest request) {
            return base.Channel.calculate(request);
        }
        
        public ConsoleTester.ServiceReference1.Solution calculate(ConsoleTester.ServiceReference1.BoxesArgument[] boxesString, ConsoleTester.ServiceReference1.ContainersArgument[] containersString) {
            ConsoleTester.ServiceReference1.calculateRequest inValue = new ConsoleTester.ServiceReference1.calculateRequest();
            inValue.Body = new ConsoleTester.ServiceReference1.calculateRequestBody();
            inValue.Body.boxesString = boxesString;
            inValue.Body.containersString = containersString;
            ConsoleTester.ServiceReference1.calculateResponse retVal = ((ConsoleTester.ServiceReference1.ServiceSoap)(this)).calculate(inValue);
            return retVal.Body.calculateResult;
        }
    }
}
