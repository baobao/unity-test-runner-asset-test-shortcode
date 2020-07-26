using System;
using System.IO;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using Assert = UnityEngine.Assertions.Assert;

/// <summary>
/// TestRunner AssetTest
/// </summary>
public class AssetTest
{
    [Test]
    public void AssetValidate()
    {
        var assetDirectoryPath = "Assets/AssetBundles/";
        var filePaths = Directory.GetFiles(assetDirectoryPath, "*.asset");

        bool isSuccess = true;
        foreach (var path in filePaths)
        {
            if (Validate(path) == false)
            {
                isSuccess = false;
            }
        }

        Assert.IsTrue(isSuccess);
    }

    [Test]
    public void AssetValidateUseAssert()
    {
        var assetDirectoryPath = "Assets/AssetBundles/";
        var filePaths = Directory.GetFiles(assetDirectoryPath, "*.asset");

        bool isSuccess = true;
        foreach (var path in filePaths)
        {
            if (ValidateUseAssert(path) == false)
            {
                isSuccess = false;
            }
        }

        Assert.IsTrue(isSuccess);
    }

    /// <summary>
    /// Validate
    /// </summary>
    private bool Validate(string path)
    {
        var fileName = Path.GetFileName(path);
        var asset = AssetDatabase.LoadAssetAtPath<QuestAsset>(path);

        if (asset == null)
        {
            Debug.LogError($"{fileName} => assetがnullです");
            return false;
        }

        if (string.IsNullOrEmpty(asset.id))
        {
            Debug.LogError($"{fileName} => IDが未入力です");
            return false;
        }

        if (asset.id.IndexOf("A", StringComparison.Ordinal) != 0)
        {
            Debug.LogError($"{fileName} => 命名規則が違います : {asset.id}");
            return false;
        }

        return true;
    }

    /// <summary>
    /// Validate use UnityEngine.Assertions.Assert
    /// </summary>
    private bool ValidateUseAssert(string path)
    {
        var fileName = Path.GetFileName(path);
        var asset = AssetDatabase.LoadAssetAtPath<QuestAsset>(path);

        Assert.IsNotNull(asset, $"{fileName} => asset is null");

        Assert.IsFalse(string.IsNullOrEmpty(asset.id), $"{fileName} => ID is null or empty");

        Assert.IsFalse(asset.id.IndexOf("A", StringComparison.Ordinal) != 0, $"{fileName} => The naming conventions are different. : {asset.id}");

        return true;
    }
}