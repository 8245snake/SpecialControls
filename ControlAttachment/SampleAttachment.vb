﻿Option Explicit On
Option Strict On

Imports System.Windows.Forms

''' <summary>
''' サンプル用アタッチメント
''' </summary>
Friend Class SampleAttachment
    Inherits NativeWindow

    Private _TargetControl As Control

    Private Const WM_PAINT = &HF
    Private Const WM_NCPAINT = &H85


    Public Sub New(targetControl As Control)
        _TargetControl = targetControl

        AddHandler _TargetControl.HandleCreated, AddressOf OnHandleCreated
        AddHandler _TargetControl.HandleDestroyed, AddressOf OnHandleDestroyed

        If _TargetControl.IsHandleCreated Then
            AssignHandle(_TargetControl.Handle)
        End If

    End Sub

    Public Overrides Sub ReleaseHandle()
        RemoveHandler _TargetControl.HandleCreated, AddressOf OnHandleCreated
        RemoveHandler _TargetControl.HandleDestroyed, AddressOf OnHandleDestroyed
        _TargetControl = Nothing
        MyBase.ReleaseHandle()
    End Sub

    Private Sub OnHandleCreated(sender As Object, e As EventArgs)
        Dim ctrl = TryCast(sender, Control)
        AssignHandle(ctrl.Handle)
    End Sub

    Private Sub OnHandleDestroyed(sender As Object, e As EventArgs)
        ReleaseHandle()
    End Sub

    Protected Overrides Sub WndProc(ByRef m As Message)
        MyBase.WndProc(m)

        Select Case m.Msg
            Case WM_PAINT, WM_NCPAINT
        End Select

    End Sub
End Class