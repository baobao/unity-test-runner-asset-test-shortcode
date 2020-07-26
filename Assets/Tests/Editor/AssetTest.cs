using System;
using System.IO;
using NUnit.Framework;
using UnityEditor;
using Assert = UnityEngine.Assertions.Assert;

/// <summary>
/// TestRunner AssetTest
/// </summary>
public class AssetTest
{

    [Test]
    public void AssetValidate()
    {
        // Don't stop the process with Assert
        Assert.raiseExceptions = false;

        var assetDirectoryPath = "Assets/AssetBundles/";
        var filePaths = Directory.GetFiles(assetDirectoryPath, "*.asset");

        foreach (var path in filePaths)
        {
            Validate(path);
        }
    }

    private void Validate(string path)
    {
        var fileName = Path.GetFileName(path);
        var asset = AssetDatabase.LoadAssetAtPath<QuestAsset>(path);

        Assert.IsNotNull(asset, $"{fileName} => asset is null");

        Assert.IsFalse(string.IsNullOrEmpty(asset.id),
            $"{fileName} => ID is null or empty");

        Assert.IsFalse(asset.id.IndexOf("A", StringComparison.Ordinal) != 0,
            $"{fileName} => The naming conventions are different. : {asset.id}");
    }
}