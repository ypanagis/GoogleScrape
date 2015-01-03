Public Class Result

    Dim _query As String
    Dim _url As String

    Public Sub New(ByVal query As String, ByVal url As String)
        _query = query
        _url = url
    End Sub

    Public Property Query() As String
        Get
            Return _query
        End Get
        Set(value As String)
            _query = value
        End Set
    End Property

    Public Property Url() As String
        Get
            Return _url
        End Get
        Set(value As String)
            _url = value
        End Set
    End Property


End Class
