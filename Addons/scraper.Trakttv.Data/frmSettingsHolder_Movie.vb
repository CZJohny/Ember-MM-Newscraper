﻿' ################################################################################
' #                             EMBER MEDIA MANAGER                              #
' ################################################################################
' ################################################################################
' # This file is part of Ember Media Manager.                                    #
' #                                                                              #
' # Ember Media Manager is free software: you can redistribute it and/or modify  #
' # it under the terms of the GNU General Public License as published by         #
' # the Free Software Foundation, either version 3 of the License, or            #
' # (at your option) any later version.                                          #
' #                                                                              #
' # Ember Media Manager is distributed in the hope that it will be useful,       #
' # but WITHOUT ANY WARRANTY; without even the implied warranty of               #
' # MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the                #
' # GNU General Public License for more details.                                 #
' #                                                                              #
' # You should have received a copy of the GNU General Public License            #
' # along with Ember Media Manager.  If not, see <http://www.gnu.org/licenses/>. #
' ################################################################################

Imports System.IO
Imports EmberAPI

Public Class frmSettingsHolder_Movie

#Region "Events"

    Public Event ModuleSettingsChanged()

    Public Event SetupScraperChanged(ByVal state As Boolean, ByVal difforder As Integer)

#End Region 'Events

#Region "Methods"

    Private Sub btnDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDown.Click
        Dim order As Integer = ModulesManager.Instance.externalScrapersModules_Data_Movie.FirstOrDefault(Function(p) p.AssemblyName = Trakttv_Data._AssemblyName).ModuleOrder
        If order < ModulesManager.Instance.externalScrapersModules_Data_Movie.Count - 1 Then
            ModulesManager.Instance.externalScrapersModules_Data_Movie.FirstOrDefault(Function(p) p.ModuleOrder = order + 1).ModuleOrder = order
            ModulesManager.Instance.externalScrapersModules_Data_Movie.FirstOrDefault(Function(p) p.AssemblyName = Trakttv_Data._AssemblyName).ModuleOrder = order + 1
            RaiseEvent SetupScraperChanged(chkEnabled.Checked, 1)
            orderChanged()
        End If
    End Sub

    Private Sub btnUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp.Click
        Dim order As Integer = ModulesManager.Instance.externalScrapersModules_Data_Movie.FirstOrDefault(Function(p) p.AssemblyName = Trakttv_Data._AssemblyName).ModuleOrder
        If order > 0 Then
            ModulesManager.Instance.externalScrapersModules_Data_Movie.FirstOrDefault(Function(p) p.ModuleOrder = order - 1).ModuleOrder = order
            ModulesManager.Instance.externalScrapersModules_Data_Movie.FirstOrDefault(Function(p) p.AssemblyName = Trakttv_Data._AssemblyName).ModuleOrder = order - 1
            RaiseEvent SetupScraperChanged(chkEnabled.Checked, -1)
            orderChanged()
        End If
    End Sub

    Private Sub chkEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles chkEnabled.CheckedChanged
        RaiseEvent SetupScraperChanged(chkEnabled.Checked, 0)
    End Sub
    Private Sub chkRating_CheckedChanged(sender As Object, e As EventArgs) Handles chkRating.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub
    Private Sub chkPlaycount_CheckedChanged(sender As Object, e As EventArgs) Handles chkPlaycount.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub
    Private Sub chkLastPlayed_CheckedChanged(sender As Object, e As EventArgs) Handles chkLastPlayed.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub
    Private Sub txtTraktUser_TextChanged(sender As Object, e As EventArgs) Handles txtTraktUser.TextChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub
    Private Sub txtTraktPassword_TextChanged(sender As Object, e As EventArgs) Handles txtTraktPassword.TextChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub
    Private Sub chkUsePersonalRatings_CheckedChanged(sender As Object, e As EventArgs) Handles chkUsePersonalRatings.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Public Sub New()
        InitializeComponent()
        Me.SetUp()
    End Sub

    Sub orderChanged()
        Dim order As Integer = ModulesManager.Instance.externalScrapersModules_Data_Movie.FirstOrDefault(Function(p) p.AssemblyName = Trakttv_Data._AssemblyName).ModuleOrder
        If ModulesManager.Instance.externalScrapersModules_Data_Movie.Count > 1 Then
            btnDown.Enabled = (order < ModulesManager.Instance.externalScrapersModules_Data_Movie.Count - 1)
            btnUp.Enabled = (order > 0)
        Else
            btnDown.Enabled = False
            btnUp.Enabled = False
        End If
    End Sub

    Private Sub SetUp()
        Me.chkRating.Text = Master.eLang.GetString(400, "Rating")
        Me.chkEnabled.Text = Master.eLang.GetString(774, "Enabled")
        Me.gbScraperFieldsOpts.Text = Master.eLang.GetString(791, "Scraper Fields - Scraper specific")
        Me.gbScraperOpts.Text = Master.eLang.GetString(1186, "Scraper Options")
        Me.lblInfoBottom.Text = String.Format(Master.eLang.GetString(790, "These settings are specific to this module.{0}Please refer to the global settings for more options."), Environment.NewLine)
        Me.lblScraperOrder.Text = Master.eLang.GetString(168, "Scrape Order")
        Me.chkPlaycount.Text = Master.eLang.GetString(1452, "Playcount")
        Me.chkLastPlayed.Text = Master.eLang.GetString(1369, "Last watched")
        Me.chkUsePersonalRatings.Text = Master.eLang.GetString(1464, "Use personal rating (if available)")
        Me.lblTraktPassword.Text = Master.eLang.GetString(426, "Password")
        Me.lblTraktUser.Text = Master.eLang.GetString(425, "Username")
        Me.txtTraktPassword.PasswordChar = "*"c
    End Sub

#End Region 'Methods

End Class