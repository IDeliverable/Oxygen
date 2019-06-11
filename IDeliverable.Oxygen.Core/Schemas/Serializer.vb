Imports System.IO
Imports System.Text
Imports System.Xml
Imports System.Xml.Serialization


Public Class Serializer(Of T)


	Public Shared Function DeserializeXml(ByVal xml As String) As T
		Dim serializer As New XmlSerializer(GetType(T))
		Using reader As New StringReader(xml)
			Dim newEntity As T = DirectCast(serializer.Deserialize(reader), T)
			Return newEntity
		End Using
	End Function


	Public Shared Function DeserializeFile(ByVal filePath As String) As T
		Dim serializer As New XmlSerializer(GetType(T))
		Using reader As New StreamReader(filePath)
			Dim newObject As T = DirectCast(serializer.Deserialize(reader), T)
			Return newObject
		End Using
	End Function


	Public Shared Function DeserializeOxe(ByVal filePath As String) As Entity
		Return Serializer(Of Entity).DeserializeFile(filePath)
	End Function


	Public Shared Function SerializeToXml(ByVal o As T, Optional ByVal omitDeclaration As Boolean = False) As String
		Dim builder As New StringBuilder()
		Using sw As New StringWriter(builder)
			Dim serializer As XmlSerializer = New XmlSerializer(GetType(T))
			Dim settings As New XmlWriterSettings()
			With settings
				.OmitXmlDeclaration = omitDeclaration
				.Indent = True
				.IndentChars = vbTab
				' NOTE: The Encoding property cannot be specified when writing 
				' to a StringWriter; UTF-16 will always be used.
			End With
			Using writer = XmlWriter.Create(sw, settings)
				serializer.Serialize(writer, o)
				Return builder.ToString()
			End Using
		End Using
	End Function


	Public Shared Sub SerializeToFile(ByVal o As T, ByVal filePath As String, Optional ByVal omitDeclaration As Boolean = False)
		Dim settings As New XmlWriterSettings()
		With settings
			.OmitXmlDeclaration = omitDeclaration
			.Indent = True
			.IndentChars = vbTab
			.Encoding = Encoding.UTF8
		End With
		Using writer = XmlWriter.Create(filePath, settings)
			Dim serializer As XmlSerializer = New XmlSerializer(GetType(T))
			serializer.Serialize(writer, o)
		End Using
	End Sub


	Public Shared Sub SerializeToOxe(ByVal e As Entity, ByVal filePath As String)
		Serializer(Of Entity).SerializeToFile(e, filePath, False)
	End Sub


End Class
