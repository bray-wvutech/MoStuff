﻿using Domain.Models;
using Domain.Models.ShoppingCart;

namespace UI.Utilities;

public static class PictureHelper
{
    private const string PLACE_HOLDER_URI = "/images/PlaceHolder.png";

    public static string SafeItemPicture(StoreItem? item)
    {
        if (item == null)
        {
            return PLACE_HOLDER_URI;
        }

        if (string.IsNullOrEmpty(item.PictureUri))
        {
            return PLACE_HOLDER_URI;
        }

        return item.PictureUri;
    }

    public static string SafeItemPicture(ShoppingCartItem? item)
    {
        if (item == null)
        {
            return PLACE_HOLDER_URI;
        }

        if (string.IsNullOrEmpty(item.PictureUri))
        {
            return PLACE_HOLDER_URI;
        }

        return item.PictureUri;
    }

    public static string UploadNewImage(IWebHostEnvironment environment,
        IFormFile file)
    {
        // this creates a random unique id
        string guid = Guid.NewGuid().ToString();

        // we get what the extension of the file we chose was
        // it should proably be a jpeg, png, or gif
        // but we aren't doing any validation of the file type
        string ext = Path.GetExtension(file.FileName);

        // get the short path
        // which is the relative path to our image folder plus
        // "guid.ext"
        // so something like
        // "images\\Ietms\\08a783ba4567-4f3c-8e7d-4f3c-8e7d.jpg"
        string shortPath = Path.Combine("images\\Items", guid + ext);

        // we need the full path not just the relative path to save the file
        // that is what we get from the environment variable
        string path = Path.Combine(environment.WebRootPath, shortPath);

        // copy the file to our images folder
        // with the "guid.ext" file name
        using (var fs = new FileStream(path, FileMode.Create))
        {
            file.CopyTo(fs);
        }

        // we want the root path to the image
        // so add that to the short path before returning
        shortPath = Path.Combine("\\", shortPath);
        return shortPath;
    }
}
