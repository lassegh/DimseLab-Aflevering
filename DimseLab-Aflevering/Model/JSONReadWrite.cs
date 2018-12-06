﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Popups;
using DimseLab_Aflevering.Model;
using Newtonsoft.Json;

namespace DimseLab_Aflevering
{
    class JsonReadWrite
    {

        private static string JsonFileName = "ProjectModel.json";



        /// <summary>
        /// Gemmer projektets data
        /// </summary>
        /// <param name="notes"></param>
        public static async void SaveNotesAsJsonAsync(ObservableCollection<Project> notes)
        {
            string notesJsonString = JsonConvert.SerializeObject(notes);
            SerializeNotesFileAsync(notesJsonString, JsonFileName);
        }



        /// <summary>
        /// Loader projektets data
        /// </summary>
        /// <returns></returns>
        public static async Task<ObservableCollection<Project>> LoadNotesFromJsonAsync()
        {
            try
            {
                string notesJsonString = await DeserializeNotesFileAsync(JsonFileName);
                    return (ObservableCollection<Project>)JsonConvert.DeserializeObject(notesJsonString, typeof(ObservableCollection<Project>));
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return new ObservableCollection<Project>();

        }



        // motode der kaldes i SaveNotesAsJsonAsync()
        private static async void SerializeNotesFileAsync(string notesJsonString, string fileName)
        {
            StorageFile localFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(localFile, notesJsonString);
        }



        // metode der kaldes i LoadNotesFromJsonAsync()
        private static async Task<string> DeserializeNotesFileAsync(string fileName)
        {
            try
            {
                StorageFile localFile = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                return await FileIO.ReadTextAsync(localFile);
            }
            catch (FileNotFoundException ex)
            {
                MessageDialogHelper.Show("Loading for the first time? - Try Add and Save some Notes before trying to Save for the first time", "File not Found");
                return null;
            }
        }



        /// <summary>
        /// message helper der giver besked første gang du loader programmet
        /// </summary>
        private class MessageDialogHelper
        {
            public static async void Show(string content, string title)
            {
                MessageDialog messageDialog = new MessageDialog(content, title);
                await messageDialog.ShowAsync();
            }
        }

    }
}