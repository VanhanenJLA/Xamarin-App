﻿using Android.Media;
using System;

namespace DesignApp.Droid.Camera2
{
	public class ImageAvailableListener : Java.Lang.Object, ImageReader.IOnImageAvailableListener
	{
		public event EventHandler<byte[]> Photo;

		public void OnImageAvailable(ImageReader reader)
		{
			Image image = null;

			try
			{
				image = reader.AcquireLatestImage();
				var buffer = image.GetPlanes()[0].Buffer;
				var imageData = new byte[buffer.Capacity()];
				buffer.Get(imageData);

				Photo?.Invoke(this, imageData);
			}
			catch (Exception)
			{
				// ignored
			}
			finally
			{
				image?.Close();
			}
		}
	}
}
