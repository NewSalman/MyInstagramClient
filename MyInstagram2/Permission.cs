using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MyInstagram2
{
    public class Permission
    {
        public static async Task CheckPermission()
        {
            var StorageStatus = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
            var CameraStatus = await Permissions.CheckStatusAsync<Permissions.Camera>();
            var WriteStatus = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();

            if(StorageStatus != PermissionStatus.Granted || WriteStatus != PermissionStatus.Granted || 
                CameraStatus != PermissionStatus.Granted)
            {
                await GetPermission();
            }
            return;
        }

        private static async Task GetPermission()
        {
            await Permissions.RequestAsync<Permissions.Camera>();
            await Permissions.RequestAsync<Permissions.StorageWrite>();
            await Permissions.RequestAsync<Permissions.StorageRead>();
        }
    }
}
