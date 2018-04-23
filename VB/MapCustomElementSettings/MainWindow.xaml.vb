Imports System.Collections.Generic
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Documents
Imports System.Xml
Imports System.Xml.Serialization
Imports DevExpress.Xpf.Map

Namespace MapCustomElementSettings
    Partial Public Class MainWindow
        Inherits Window

        Private Const filepath As String = "..//..//disasters.xml"


        Private disasters_Renamed As List(Of Disaster)
        Public ReadOnly Property Disasters() As List(Of Disaster)
            Get
                Return disasters_Renamed
            End Get
        End Property

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub Window_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            disasters_Renamed = DeserializeXml(filepath)
            Me.DataContext = Me
        End Sub

        Private Function DeserializeXml(ByVal filepath As String) As List(Of Disaster)
            Dim serializer As New XmlSerializer(GetType(List(Of Disaster)))
            Dim reader As XmlReader = New XmlTextReader(filepath)
            Dim data As List(Of Disaster) = DirectCast(serializer.Deserialize(reader), List(Of Disaster))
            reader.Close()

            Return data
        End Function
    End Class

    Friend Class MarkerTemplateSelector
        Inherits DataTemplateSelector

        Public Property AirRaidsDataTemplate() As DataTemplate
        Public Property BushfiresDataTemplate() As DataTemplate
        Public Property CycloneDataTemplate() As DataTemplate
        Public Property SinkingDataTemplate() As DataTemplate
        Public Property EpidemicDataTemplate() As DataTemplate

        Public Overrides Function SelectTemplate(ByVal item As Object, ByVal container As DependencyObject) As DataTemplate
            Dim disaster As Disaster = TryCast(item, Disaster)
            If disaster Is Nothing Then
                Return MyBase.SelectTemplate(item, container)
            End If

            Dim element As MapCustomElement = TryCast(container, MapCustomElement)
            Select Case disaster.Type
                Case "Air Raids"
                    Return AirRaidsDataTemplate
                Case "Bushfires"
                    Return BushfiresDataTemplate
                Case "Cyclone"
                    Return CycloneDataTemplate
                Case "Sinking"
                    Return SinkingDataTemplate
                Case "Epidemic"
                    Return EpidemicDataTemplate
                Case Else
                    Return MyBase.SelectTemplate(item, container)
            End Select
        End Function
    End Class

    Public Class Disaster
        Public Property Type() As String
        Public Property LocationLatitude() As Double
        Public Property LocationLongitude() As Double
        Public Property LocationName() As String
        Public Property Deaths() As Integer
    End Class
End Namespace
