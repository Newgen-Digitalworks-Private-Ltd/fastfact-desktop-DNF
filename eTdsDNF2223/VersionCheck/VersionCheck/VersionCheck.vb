Imports System.Net
Imports VersionCheck.ApplicationMain
Public Class VersionCheck
#Region "Variables"
    Private StrLink As String = String.Empty
    Private StrDownloadLink As String = String.Empty
    Private StrAppVersion As String = String.Empty
    Private StrWebVersion As String = String.Empty
    Private BoolOlder As Boolean = False
#End Region
#Region "Property"
    Public ReadOnly Property DownloadLink
        Get
            Return StrDownloadLink
        End Get
    End Property
    Public ReadOnly Property Link As String
        Get
            Return StrLink
        End Get
    End Property
    Public ReadOnly Property AppVersion As String
        Get
            Return StrAppVersion
        End Get
    End Property
    Public ReadOnly Property WebVersion As String
        Get
            Return StrWebVersion
        End Get
    End Property
    Public ReadOnly Property Older As Boolean
        Get
            Return BoolOlder
        End Get
    End Property
#End Region
    Sub New(_StrLink As String, _StrDownloadLink As String, _StrAppVersion As String)
        Try
            StrLink = _StrLink : StrDownloadLink = _StrDownloadLink
            StrAppVersion = _StrAppVersion
            If System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable() Then
                oReturnStatus = ValidVersion(BoolOlder)
                If Not oReturnStatus.BlnReturn Then Throw New Exception(oReturnStatus.StrReturn)
            Else
                BoolOlder = False
            End If
        Catch ex As Exception
            BoolOlder = False
        End Try
    End Sub
    Private Function ValidVersion(ByRef blnolder As Boolean) As ReturnStatus
        Try
            Dim oWebClient As New WebClient
            StrWebVersion = oWebClient.DownloadString(Link)
            oWebClient.Dispose()
            StrAppVersion = Application.ProductVersion
            If WebVersion > AppVersion Then
                blnolder = True
            Else
                blnolder = False
            End If
            Return New ReturnStatus(True)
        Catch ex As Exception
            Return New ReturnStatus(False)
        End Try
    End Function
End Class
