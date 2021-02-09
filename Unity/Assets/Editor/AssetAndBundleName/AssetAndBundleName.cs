using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using System.Text;
using ETModel;

namespace ETEditor
{
    public class BundleConfig
    {
        public long Id { get; set; }
        public Dictionary<string, string> Relation;
    }

    public class AssetAndBundleName
    {
        const string BundleConfigPath = @"./Assets/Res/Config/BundleConfig.txt";

        [MenuItem("Tools/生成asset 与 bundle 关系")]
        public static void GenRelation()
        {
            BundleConfig bundleConfig = new BundleConfig();
            bundleConfig.Id = 1;
            bundleConfig.Relation = new Dictionary<string, string>();

            var bundleNames = AssetDatabase.GetAllAssetBundleNames();
            foreach (string bundle in bundleNames)
            {
                var assetPaths = AssetDatabase.GetAssetPathsFromAssetBundle(bundle);
                foreach (string path in assetPaths)
                {
                    string asset = Path.GetFileNameWithoutExtension(path);
                    bundleConfig.Relation[asset] = bundle;
                }
            }
            
            string json = JsonHelper.ToJson(bundleConfig);
            File.WriteAllText(BundleConfigPath, json);
            AssetDatabase.Refresh();
            Debug.Log($"{json}");
        }
    }
}