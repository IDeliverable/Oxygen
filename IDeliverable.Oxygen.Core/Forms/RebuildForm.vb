Imports EnvDTE
Imports EnvDTE80
Imports System.Collections.Generic
Imports System.Windows.Forms


Public Class RebuildForm


	Public Shared Sub RebuildEntities(ByVal dte As DTE2, ByVal proj As Project, ByVal items As List(Of ProjectItem))

		If items IsNot Nothing AndAlso items.Count > 0 Then

			Dim f As New RebuildForm()
			f.DTE = dte
			f.Project = proj
			f.Items = items

			f.ShowDialog()

			If f.ErrorsOccurred Then
				MessageBox.Show("One or more errors occurred while rebuilding entities. Check the Oxygen pane in the Output window for details.", "Rebuild", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
			End If

		End If

	End Sub


	Public DTE As DTE2
	Public Project As Project
	Public Items As List(Of ProjectItem)
	Public ErrorsOccurred As Boolean = False

	Private m_Thread As Threading.Thread
	Private m_IsCancelled As Boolean = False


	Private Sub RebuildForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

		SyncLock Me
			m_IsCancelled = True
		End SyncLock
		' Wait 10 seconds for the thread to terminate, then abort it.
		If m_Thread IsNot Nothing Then
			If Not m_Thread.Join(10000) Then
				m_Thread.Abort()
				' Wait another 5 seconds for the thread to abort.
				m_Thread.Join(5000)
			End If
		End If

	End Sub


	Private Sub RebuildForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

		ProgressBar1.Minimum = 0
		ProgressBar1.Maximum = Items.Count
		ProgressBar1.Step = 1
		Dim m_Thread As New Threading.Thread(AddressOf DoRebuild)
		m_Thread.IsBackground = True
		m_Thread.Start()
		TheCancelButton.Focus()

	End Sub


	Private Sub DoRebuild()

		If Items.Count > 0 Then
			Dim g As New Generator(DTE, Items(0).ContainingProject)
			For Each item As ProjectItem In Items
				Invoke(New UpdateProgressDelegate(AddressOf UpdateProgress), item.Name)
				Try
					If g.GenerateEntity(item) = False Then
						ErrorsOccurred = True
					End If
				Catch
				End Try
				SyncLock Me
					If m_IsCancelled Then
						Return
					End If
				End SyncLock
			Next
		End If
		Invoke(New MethodInvoker(AddressOf Close))

	End Sub


	Private Delegate Sub UpdateProgressDelegate(ByVal fileName As String)


	Private Sub UpdateProgress(ByVal fileName As String)

		ProgressLabel.Text = String.Format("Rebuilding entity {0}...", fileName)
		ProgressBar1.PerformStep()

	End Sub


End Class

