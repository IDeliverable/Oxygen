Imports EnvDTE
Imports EnvDTE80
Imports CodeSmith.Engine
Imports System.CodeDom.Compiler
Imports System.IO
Imports System.Text


Public Class Generator


	''' <summary>
	''' Creates a new instance of the Generator class using the Master template in the specified project.
	''' </summary>
	''' <param name="dte">The DTE object representing the current Visual Studio instance.</param>
	''' <param name="project">The Project object representing the template in which generation will occur.</param>
	''' <remarks></remarks>
	Sub New(ByVal dte As DTE2, ByVal project As Project)

		m_DTE = dte

		' We will direct any information output to a dedicated output 
		' window pane. Use existing or create new.
		Try
			m_OutputWindowPane = m_DTE.ToolWindows.OutputWindow.OutputWindowPanes.Item("Oxygen")
		Catch ex As ArgumentException
			m_OutputWindowPane = m_DTE.ToolWindows.OutputWindow.OutputWindowPanes.Add("Oxygen")
		End Try

		Try

			' Get project folder and path template path. Perhaps the relative 
			' master template file path should not be hard coded...
			m_ProjectFolderPath = Path.GetDirectoryName(project.FullName)
			m_TemplateFilePath = Path.Combine(m_ProjectFolderPath, "Templates\Master.cst")

			' Make sure template file exists.
			If Not File.Exists(m_TemplateFilePath) Then
				Throw New Exception(String.Format("The template file '{0}' does not exists.", m_TemplateFilePath))
			End If

			' Compile the template and check for errors.
			m_Compiler = New CodeTemplateCompiler(m_TemplateFilePath)
			m_Compiler.Compile()
			If m_Compiler.Errors.Count > 0 Then
				Dim sb As New StringBuilder()
				For Each ce As CompilerError In m_Compiler.Errors
					sb.AppendLine(ce.ToString())
				Next
				Throw New Exception(String.Format("Errors occurred while compiling the template '{0}'." & vbCrLf & "{1}", m_TemplateFilePath, sb.ToString()))
			End If

			' Create a template instance.
			m_Template = m_Compiler.CreateInstance()

		Catch ex As Exception
			m_OutputWindowPane.OutputString(String.Format("An unexpected error occurred while creating entity generator for project '{0}'." & vbCrLf & "{1}" & vbCrLf, project.FullName, ex.ToString()))
			m_OutputWindowPane.Activate()
		End Try

	End Sub


	Private m_DTE As DTE2
	Private m_OutputWindowPane As OutputWindowPane
	Private m_ProjectFolderPath As String
	Private m_TemplateFilePath As String
	Private m_Compiler As CodeTemplateCompiler
	Private m_Template As CodeTemplate


	''' <summary>
	''' Performs code generation on the specified entity using the CodeSmith API.
	''' </summary>
	''' <param name="item">The ProjectItem object representing the entity file to generate.</param>
	''' <returns>True if the entity was successfully generated, false if an error occurred during generation.</returns>
	''' <remarks></remarks>
	Public Function GenerateEntity(ByVal item As ProjectItem) As Boolean

		m_OutputWindowPane.OutputString(String.Format("Building entity file {0}...", item.FileNames(0)))

		Try

			' Get entity file path and deserialize the entity file to find 
			' the reference to the settings file to use.
			Dim entityFilePath As String = item.FileNames(0)
			Dim settingsFilePath As String = Serializer(Of Entity).DeserializeOxe(entityFilePath).SettingsFilePath

			' Instantiate the template and set its properties.
			m_Template.SetProperty("Entity", entityFilePath)
			m_Template.SetProperty("Settings", Path.Combine(m_ProjectFolderPath, settingsFilePath))

			' Create output file path, make sure file is writeable, and 
			' render the output. Add the output file as a project item below 
			' the entity file.
			Dim outputPath As String = Path.Combine(m_ProjectFolderPath, Path.ChangeExtension(entityFilePath, ".vb"))
			MakeWriteable(outputPath)
			m_Template.RenderToFile(outputPath, True)
			item.ProjectItems.AddFromFile(outputPath)

			m_OutputWindowPane.OutputString(" Done!" & vbCrLf)

			Return True

		Catch ex As Exception
			m_OutputWindowPane.OutputString(String.Format(vbCrLf & "An unexpected error occurred while building the entity file '{0}'." & vbCrLf & "{1}" & vbCrLf, item.FileNames(0), ex.ToString()))
			m_OutputWindowPane.Activate()
		End Try

		Return False

	End Function


	''' <summary>
	''' Makes sure a file is writeable by checking it out of source control.
	''' </summary>
	''' <param name="filePath"></param>
	''' <remarks></remarks>
	Protected Sub MakeWriteable(ByVal filePath As String)

		Dim scc As SourceControl2 = DirectCast(m_DTE.SourceControl, SourceControl2)
		If scc.IsItemUnderSCC(filePath) Then
			If Not scc.IsItemCheckedOut(filePath) Then
				If Not scc.CheckOutItem2(filePath, vsSourceControlCheckOutOptions.vsSourceControlCheckOutOptionLocalVersion) Then
					Throw New Exception(String.Format("File '{0}' is under source control and could not be checked out.", filePath))
				End If
			End If
		End If

	End Sub


End Class

