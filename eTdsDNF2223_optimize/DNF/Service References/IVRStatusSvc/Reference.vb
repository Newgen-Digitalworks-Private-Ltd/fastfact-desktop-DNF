﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Namespace IVRStatusSvc
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ServiceModel.ServiceContractAttribute([Namespace]:="http://incometaxindiaefiling.gov.in/ditws/ITRVStatus/v_1_0", ConfigurationName:="IVRStatusSvc.itrvstatusPort")>  _
    Public Interface itrvstatusPort
        
        'CODEGEN: Generating message contract since the operation getITRVStatus is neither RPC nor document wrapped.
        <System.ServiceModel.OperationContractAttribute(Action:="", ReplyAction:="*"),  _
         System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults:=true)>  _
        Function getITRVStatus(ByVal request As IVRStatusSvc.getITRVStatusRequest1) As IVRStatusSvc.getITRVStatusResponse1
    End Interface
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0"),  _
     System.SerializableAttribute(),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=true, [Namespace]:="http://incometaxindiaefiling.gov.in/ditws/ITRVStatus/v_1_0")>  _
    Partial Public Class getITRVStatusRequest
        Inherits Object
        Implements System.ComponentModel.INotifyPropertyChanged
        
        Private pANField As String
        
        Private assessmentYearField As String
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=0)>  _
        Public Property PAN() As String
            Get
                Return Me.pANField
            End Get
            Set
                Me.pANField = value
                Me.RaisePropertyChanged("PAN")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=1)>  _
        Public Property AssessmentYear() As String
            Get
                Return Me.assessmentYearField
            End Get
            Set
                Me.assessmentYearField = value
                Me.RaisePropertyChanged("AssessmentYear")
            End Set
        End Property
        
        Public Event PropertyChanged As System.ComponentModel.PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
        
        Protected Sub RaisePropertyChanged(ByVal propertyName As String)
            Dim propertyChanged As System.ComponentModel.PropertyChangedEventHandler = Me.PropertyChangedEvent
            If (Not (propertyChanged) Is Nothing) Then
                propertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(propertyName))
            End If
        End Sub
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0"),  _
     System.SerializableAttribute(),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=true, [Namespace]:="http://incometaxindiaefiling.gov.in/ditws/ITRVStatus/v_1_0")>  _
    Partial Public Class getITRVStatusResponse
        Inherits Object
        Implements System.ComponentModel.INotifyPropertyChanged
        
        Private resultField As String
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=0)>  _
        Public Property result() As String
            Get
                Return Me.resultField
            End Get
            Set
                Me.resultField = value
                Me.RaisePropertyChanged("result")
            End Set
        End Property
        
        Public Event PropertyChanged As System.ComponentModel.PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
        
        Protected Sub RaisePropertyChanged(ByVal propertyName As String)
            Dim propertyChanged As System.ComponentModel.PropertyChangedEventHandler = Me.PropertyChangedEvent
            If (Not (propertyChanged) Is Nothing) Then
                propertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(propertyName))
            End If
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.ServiceModel.MessageContractAttribute(IsWrapped:=false)>  _
    Partial Public Class getITRVStatusRequest1
        
        <System.ServiceModel.MessageBodyMemberAttribute([Namespace]:="http://incometaxindiaefiling.gov.in/ditws/ITRVStatus/v_1_0", Order:=0)>  _
        Public getITRVStatusRequest As IVRStatusSvc.getITRVStatusRequest
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal getITRVStatusRequest As IVRStatusSvc.getITRVStatusRequest)
            MyBase.New
            Me.getITRVStatusRequest = getITRVStatusRequest
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.ServiceModel.MessageContractAttribute(IsWrapped:=false)>  _
    Partial Public Class getITRVStatusResponse1
        
        <System.ServiceModel.MessageBodyMemberAttribute([Namespace]:="http://incometaxindiaefiling.gov.in/ditws/ITRVStatus/v_1_0", Order:=0)>  _
        Public getITRVStatusResponse As IVRStatusSvc.getITRVStatusResponse
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal getITRVStatusResponse As IVRStatusSvc.getITRVStatusResponse)
            MyBase.New
            Me.getITRVStatusResponse = getITRVStatusResponse
        End Sub
    End Class
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Public Interface itrvstatusPortChannel
        Inherits IVRStatusSvc.itrvstatusPort, System.ServiceModel.IClientChannel
    End Interface
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Partial Public Class itrvstatusPortClient
        Inherits System.ServiceModel.ClientBase(Of IVRStatusSvc.itrvstatusPort)
        Implements IVRStatusSvc.itrvstatusPort
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String)
            MyBase.New(endpointConfigurationName)
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As String)
            MyBase.New(endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal binding As System.ServiceModel.Channels.Binding, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(binding, remoteAddress)
        End Sub
        
        <System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Function IVRStatusSvc_itrvstatusPort_getITRVStatus(ByVal request As IVRStatusSvc.getITRVStatusRequest1) As IVRStatusSvc.getITRVStatusResponse1 Implements IVRStatusSvc.itrvstatusPort.getITRVStatus
            Return MyBase.Channel.getITRVStatus(request)
        End Function
        
        Public Function getITRVStatus(ByVal getITRVStatusRequest As IVRStatusSvc.getITRVStatusRequest) As IVRStatusSvc.getITRVStatusResponse
            Dim inValue As IVRStatusSvc.getITRVStatusRequest1 = New IVRStatusSvc.getITRVStatusRequest1()
            inValue.getITRVStatusRequest = getITRVStatusRequest
            Dim retVal As IVRStatusSvc.getITRVStatusResponse1 = CType(Me,IVRStatusSvc.itrvstatusPort).getITRVStatus(inValue)
            Return retVal.getITRVStatusResponse
        End Function
    End Class
End Namespace
