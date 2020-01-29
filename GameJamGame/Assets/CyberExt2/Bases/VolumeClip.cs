using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
#pragma warning disable IDE0044
namespace Cyberevolver.Unity
{

    /// <summary>
    /// <see cref="AudioClip"/> connected with volume level.
    /// </summary>
    [Serializable]
    public class VolumeClip
    {
        [SerializeField] private AudioClip clip;
        [SerializeField]  private Percent volume;
        public AudioClip AudioClip => clip;
        public Percent Volume => volume;
        public VolumeClip(AudioClip clip, Percent volume)
        {
         
            this.clip = clip;
            this.volume = volume;
        }
    }
}