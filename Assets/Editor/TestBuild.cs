﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TestBuild
{
    [MenuItem("Build/Android")]
    public static void PerformBuild()
    {
        BuildPlayerOptions options = new BuildPlayerOptions();
        // 씬 추가
        List<string> scenes = new List<string>();
        foreach (var scene in EditorBuildSettings.scenes)
        {
            if (!scene.enabled) continue;
            scenes.Add(scene.path);
        }
        options.scenes = scenes.ToArray();
        // 타겟 경로(빌드 결과물이 여기 생성됨)
        options.locationPathName = "Build/floatDouble.apk";
        // 빌드 타겟
        options.target = BuildTarget.Android;

        // 빌드
        BuildPipeline.BuildPlayer(options);
    }

    [MenuItem("Build/iOS")]
    public static void PerformBuildiOS()
    {
        BuildPlayerOptions options = new BuildPlayerOptions();
        // 씬 추가
        List<string> scenes = new List<string>();
        foreach (var scene in EditorBuildSettings.scenes)
        {
            if (!scene.enabled) continue;
            scenes.Add(scene.path);
        }
        options.scenes = scenes.ToArray();
        // 타겟 경로(빌드 결과물이 여기 생성됨)
        options.locationPathName = "Build/floatDouble/";
        // 빌드 타겟
        options.target = BuildTarget.iOS;

        // 빌드
        BuildPipeline.BuildPlayer(options);
    }
}