// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using UnityEngine;

namespace Performance
{
    internal static class Config
    {
        public static          float    HorizontalGap = 1.5f;
        public static          float    VerticalGap   = 1.1f;
        public static          float    DefaultDelay  = 1f;
        public static          float    OutDistance   = -2f;
        public static          float    CubeScale     = 1.0f / 2.0f;
        public static readonly Material DefaultCube   = Resources.Load<Material>( "Materials/CubeWhite" );
        public static readonly Material BlueCube      = Resources.Load<Material>( "Materials/CubeBlue" );
        public static readonly Material YellowCube    = Resources.Load<Material>( "Materials/CubeYellow" );
        public static readonly Material GreenCube     = Resources.Load<Material>( "Materials/CubeGreen" );
        public static readonly Material RedCube       = Resources.Load<Material>( "Materials/CubeRed" );
    }
}