using UnityEngine;

namespace MyFps
{
    /// <summary>
    /// 사운드 데이터 관리하는 싱글톤 클래스
    /// </summary>
    public class AudioManager : Singleton<AudioManager>
    {
        #region Variables
        public Sound[] sounds;  //관리하는 사운드 목록

        private string bgmSound = "";    //현재 플레이 되고 있는 배경음 이름
        #endregion

        #region Unity Event Method
        protected override void Awake()
        {
            base.Awake();

            //사운드 목록 데이터 세팅
            foreach (var sound in sounds)
            {
                sound.source = gameObject.AddComponent<AudioSource>();

                sound.source.clip = sound.clip;
                sound.source.volume = sound.volume;
                sound.source.pitch = sound.pitch;
                sound.source.loop = sound.loop;
                sound.source.playOnAwake = sound.playOnAwake;

                
            }
        }
        #endregion

        #region Custom Method
        //사운드 플레이
        public void Play(string name)
        {
            //플레이 할 사운드
            Sound sound = null;

            foreach (var s in sounds)
            {
                if(s.name == name)
                {
                    sound = s;
                    break;          //찾았으면 반복문 정지
                }
            }
            //못 찾았으면
            if (sound == null)
            {
                Debug.Log($"cannot find {name} play sound");
                return;
            }

            sound.source.Play();
        }

        //사운드 플레이 정지
        public void Stop(string name)
        {
            //정지할 사운드
            Sound sound = null;

            foreach (var s in sounds)
            {
                if (s.name == name)
                {
                    sound = s;
                    break;          //찾았으면 반복문 정지
                }
            }
            //못 찾았으면
            if(sound == null)
            {
                Debug.Log($"cannot find {name} stop sound");
                return;
            }
            
            sound.source.Stop();
        }

        //배경음 플레이
        public void PlayBGM(string name)
        {
            //배경음 이름 체크
            if(bgmSound == name)
            {
                return;
            }

            //기본 배경음 정지 - sound : 정지할 배경음
            foreach (var s in sounds)
            {
                if (s.name == bgmSound)
                {
                    s.source.Stop();
                    break;          //찾았으면 찾은 audiSource 플레이 정지
                }
            }

            //새로운 배경음 플레이
            Sound sound = null;
            foreach (var s in sounds)
            {
                if (s.name == name)
                {
                    sound = s;
                    bgmSound = s.name;  //배경음 이름 저장
                    break;          //찾았으면 반복문 정지
                }
            }
            //못 찾았으면
            if (sound == null)
            {
                Debug.Log($"cannot find {name} BGM sound");
                return;
            }

            sound.source.Play();
        }
        #endregion
    }
}
