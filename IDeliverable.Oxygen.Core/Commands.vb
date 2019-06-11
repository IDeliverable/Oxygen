Imports System.Windows.Forms
Imports EnvDTE
Imports EnvDTE80
Imports Microsoft.VisualStudio.Shell
Imports Microsoft.VisualStudio.Shell.Interop

Public NotInheritable Class Commands

	Public Sub New(dte As DTE2, statusbar As IVsStatusbar)

		mDte = dte
		mStatusbar = statusbar

	End Sub


	Private ReadOnly mDte As DTE2
	Private ReadOnly mStatusbar As IVsStatusbar


#Region "New Entities from Database"


	Public Sub NewEntitiesFromDatabaseInvoke()

		If mDte.SelectedItems.Count = 1 Then

			Dim proj = mDte.SelectedItems.Item(1).Project
			If proj IsNot Nothing Then

				Dim foundSettingsFile = False

				For Each item As ProjectItem In proj.ProjectItems
					If item.Name.EndsWith(".oxs") Then
						foundSettingsFile = True
						Exit For
					End If
				Next

				'MessageBox.Show("This function is currently not available.", "New Entities from Database", MessageBoxButtons.OK, MessageBoxIcon.Error)

				If foundSettingsFile Then
					'Dim f As New ImportForm()
					'f.DTE = mDte
					'f.Project = proj
					'f.ShowDialog()
				Else
					MessageBox.Show("This function requires that a settings file exists in the project. Please add an Oxygen settings file to the project and try again.", "New Entities from Database", MessageBoxButtons.OK, MessageBoxIcon.Error)
				End If

			End If

		End If

	End Sub


	Public Sub NewEntitiesFromDatabaseQueryStatus(menuCommand As OleMenuCommand)

		menuCommand.Visible = False

		'If mDte.SelectedItems.Count = 1 Then
		'	Dim proj = mDte.SelectedItems.Item(1).Project
		'	If proj IsNot Nothing Then
		'		menuCommand.Visible = True
		'	End If
		'End If

	End Sub


#End Region


#Region "Rebuild All Entities"


	Public Sub RebuildAllEntitiesInvoke()

		If mDte.SelectedItems.Count = 1 Then

			Dim proj = mDte.SelectedItems.Item(1).Project
			If proj IsNot Nothing Then

				Dim items As New List(Of ProjectItem)()

				For Each item As ProjectItem In proj.ProjectItems
					If item.Name.EndsWith(".oxe") Then
						items.Add(item)
					End If
				Next

				RebuildForm.RebuildEntities(mDte, proj, items)

			End If

		End If

	End Sub


	Public Sub RebuildAllEntitiesQueryStatus(menuCommand As OleMenuCommand)

		menuCommand.Visible = False

		If mDte.SelectedItems.Count = 1 Then

			Dim proj = mDte.SelectedItems.Item(1).Project
			If proj IsNot Nothing Then
				For Each item As ProjectItem In proj.ProjectItems
					If item.Name.EndsWith(".oxe") Then
						menuCommand.Visible = True
						Exit For
					End If
				Next
			End If

		End If

	End Sub


#End Region


#Region "Edit Entity"


	Public Sub EditEntityInvoke()

		If mDte.SelectedItems.Count = 1 Then

			Dim item = mDte.SelectedItems.Item(1).ProjectItem
			If item IsNot Nothing AndAlso item.Name.EndsWith(".oxe") Then

				'Dim f As New EntityEditorForm()
				'f.DTE = mDte
				'f.Project = item.ContainingProject
				'f.Item = item
				'f.ShowDialog()

				'If f.HasChanges AndAlso f.DialogResult = DialogResult.OK Then
				'	Dim items As New List(Of ProjectItem)()
				'	items.Add(item)
				'	RebuildForm.RebuildEntities(mDte, item.ContainingProject, items)
				'End If

			End If

		End If

	End Sub


	Public Sub EditEntityQueryStatus(menuCommand As OleMenuCommand)

		menuCommand.Visible = False

		'If mDte.SelectedItems.Count = 1 Then
		'	Dim item = mDte.SelectedItems.Item(1).ProjectItem
		'	If item IsNot Nothing AndAlso item.Name.EndsWith(".oxe") Then
		'		menuCommand.Visible = True
		'	End If
		'End If

	End Sub


#End Region


#Region "Edit Settings"


	Public Sub EditSettingsInvoke()

		If mDte.SelectedItems.Count = 1 Then

			Dim item = mDte.SelectedItems.Item(1).ProjectItem
			If item IsNot Nothing AndAlso item.Name.EndsWith(".oxs") Then
				'Dim f As New SettingsEditorForm()
				'f.DTE = mDte
				'f.Project = item.ContainingProject
				'f.Item = item
				'f.ShowDialog()
			End If

		End If

	End Sub


	Public Sub EditSettingsQueryStatus(menuCommand As OleMenuCommand)

		menuCommand.Visible = False

		'If mDte.SelectedItems.Count = 1 Then
		'	Dim item = mDte.SelectedItems.Item(1).ProjectItem
		'	If item IsNot Nothing AndAlso item.Name.EndsWith(".oxs") Then
		'		menuCommand.Visible = True
		'	End If
		'End If

	End Sub


#End Region


#Region "Rebuild Entity"


	Public Sub RebuildEntityInvoke()

		Dim proj = mDte.SelectedItems.Item(1).Project
		Dim items As New List(Of ProjectItem)()

		For Each sel As SelectedItem In mDte.SelectedItems
			Dim item = sel.ProjectItem
			If item IsNot Nothing AndAlso item.Name.EndsWith(".oxe") Then
				items.Add(item)
			End If
		Next

		RebuildForm.RebuildEntities(mDte, proj, items)

	End Sub


	Public Sub RebuildEntityQueryStatus(menuCommand As OleMenuCommand)

		menuCommand.Visible = False

		For Each sel As SelectedItem In mDte.SelectedItems
			Dim item = sel.ProjectItem
			If item IsNot Nothing AndAlso Not item.Name.EndsWith(".oxe") Then
				Return
			End If
		Next

		menuCommand.Visible = True

		If mDte.SelectedItems.Count > 1 Then
			menuCommand.Text = "Rebuild Entities"
		Else
			menuCommand.Text = "Rebuild Entity"
		End If

	End Sub


#End Region


End Class
