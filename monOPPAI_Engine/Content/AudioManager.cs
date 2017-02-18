#region Namespace Scope Region
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Windows;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
#endregion

namespace monOPPAI_Engine.Content
{
    /// <summary>
    /// class for backround music and sound effect
    /// </summary>     
    public class AudioManager : GameComponent
    {
        #region Fields & Properties Region

        static AudioManager _AudMan;
        Game _gameRef;

        public static Boolean DebugFlag = false;

        AudioEngine _audioEngine;
        SoundBank _aSoundBank;

        WaveBank _seWaveBank;
        WaveBank _bgmWaveBank;

        AudioCategory _seCategory;
        AudioCategory _bgmCategory;

        float _bgmVolume = 1.0f;
        float _seVolume = 1.0f;

        Cue _bgmMusicCue;
        Cue _seMusicCue;

        #region Constant Paths
        //These are Defaults...
        const String _aEnginePath = @"Content\Audio\XACT_Geo.xgs";
        const String _aSoundBankPath = @"Content\Audio\SoundBank.xsb";
        const String _seWaveBankPath = @"Content\Audio\SE_wBank.xwb";
        const String _bgmWaveBankPath = @"Content\Audio\BGM_wBank.xwb";
        #endregion

        String _aEnginePath2;
        String _aSoundBankPath2;
        String _seWaveBankPath2;
        String _bgmWaveBankPath2;

        Boolean DefaultAudioEnginePathValues;

        
        #endregion

        #region Constructor Region

        public AudioManager(Game game)
            : base(game)
        {
            DefaultAudioEnginePathValues = true;
            try
            {
                _audioEngine = new AudioEngine(_aEnginePath);
                _aSoundBank = new SoundBank(_audioEngine, _aSoundBankPath);
                _seWaveBank = new WaveBank(_audioEngine, _seWaveBankPath);
                _bgmWaveBank = new WaveBank(_audioEngine, _bgmWaveBankPath, 0, 16);
            }
            catch (NoAudioHardwareException)
            {
                _audioEngine = null;
                _aSoundBank = null;
                _seWaveBank = null;
                _bgmWaveBank = null;
            }

            _seCategory = _audioEngine.GetCategory("SE");
            _bgmCategory = _audioEngine.GetCategory("BGM");

            _seCategory.SetVolume(_seVolume);
            _bgmCategory.SetVolume(_bgmVolume);

            _gameRef = game;
            _AudMan = this;
        }

        public AudioManager(Game game, String AudioEnginePath, String SoundBankPath, String SE_WaveBankPath, String BGM_WaveBankPath)
            : base(game)
        {
            DefaultAudioEnginePathValues = false;

            _aEnginePath2 = AudioEnginePath;
            _aSoundBankPath2 = SoundBankPath;
            _seWaveBankPath2 = SE_WaveBankPath;
            _bgmWaveBankPath2 = BGM_WaveBankPath;

            try
            {
                _audioEngine = new AudioEngine(_aEnginePath2);
                _aSoundBank = new SoundBank(_audioEngine, _aSoundBankPath2);
                _seWaveBank = new WaveBank(_audioEngine, _seWaveBankPath2);
                _bgmWaveBank = new WaveBank(_audioEngine, _bgmWaveBankPath2, 0, 16);
            }
            catch (NoAudioHardwareException)
            {
                _audioEngine = null;
                _aSoundBank = null;
                _seWaveBank = null;
                _bgmWaveBank = null;
            }

            _seCategory = _audioEngine.GetCategory("SE");
            _bgmCategory = _audioEngine.GetCategory("BGM");

            _seCategory.SetVolume(_seVolume);
            _bgmCategory.SetVolume(_bgmVolume);

            _gameRef = game;
            _AudMan = this;
        }

        public AudioManager(Game game, Object NullAudioEngineFlag)
            : base(game)
        {
            try
            {
                if (NullAudioEngineFlag.Equals(null))
                {
                    _audioEngine = null;
                    _aSoundBank = null;
                    _seWaveBank = null;
                    _bgmWaveBank = null;
                }
            }
            catch (NullReferenceException) { }
            
            _gameRef = game;
            _AudMan = this;
        }

        #endregion

        #region XNA Methods Region

        public override void Update(GameTime gameTime)
        {
            if (_AudMan._audioEngine != null || _AudMan._aSoundBank != null || 
                _AudMan._bgmWaveBank != null || _AudMan._seWaveBank != null)
            {
                _AudMan._audioEngine.Update();
                _AudMan._seCategory.SetVolume(_seVolume);
                _AudMan._bgmCategory.SetVolume(_bgmVolume);
            }
            base.Update(gameTime);            
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    if (_aSoundBank != null)
                    {
                        _aSoundBank.Dispose();
                        _aSoundBank = null;
                    }
                    if (_seWaveBank != null)
                    {
                        _seWaveBank.Dispose();
                        _seWaveBank = null;
                    }
                    if (_bgmWaveBank != null)
                    {
                        _bgmWaveBank.Dispose();
                        _bgmWaveBank = null;
                    }
                    if (_audioEngine != null)
                    {
                        _audioEngine.Dispose();
                        _audioEngine = null;
                    }
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        #endregion

        #region Local Method Region

        public static Cue Get_SEcue(String SEcueName)
        {
            if ( (_AudMan == null) || (_AudMan._audioEngine == null) || (_AudMan._aSoundBank == null) || 
                (_AudMan._seWaveBank == null))
                return null;

            return _AudMan._aSoundBank.GetCue(SEcueName);
        }

        public static void Play_SECue(String SEcueName)
        {
            if (_AudMan != null)
                if ((_AudMan._audioEngine != null) || (_AudMan._aSoundBank != null) ||
                (_AudMan._seWaveBank != null))
                {
                    if(DebugFlag)
                        Console.WriteLine("[AudioManager]Playing SE: "+ SEcueName);
                    _AudMan._aSoundBank.PlayCue(SEcueName);
                }
        }

        public static void Play_SEMusic(String MusicCueName)
        {
            if ((_AudMan == null) || (_AudMan._audioEngine == null) || (_AudMan._aSoundBank == null) ||
                (_AudMan._seWaveBank == null))
                return;

            if (_AudMan._seMusicCue != null)
                _AudMan._seMusicCue.Stop(AudioStopOptions.AsAuthored);

            _AudMan._seMusicCue = Get_SEcue(MusicCueName);

            if (_AudMan._seMusicCue != null)
            {
                if (DebugFlag)
                    Console.WriteLine("[AudioManager]Playing SE_m: " + MusicCueName);
                _AudMan._seMusicCue.Play();
            }

        }

        public static Cue Get_BGMcue(String BGMcueName)
        {
            if ((_AudMan == null) || (_AudMan._audioEngine == null) || (_AudMan._aSoundBank == null) ||
                (_AudMan._bgmWaveBank == null))
                return null;

            return _AudMan._aSoundBank.GetCue(BGMcueName);
        }

        public static void Play_BGMCue(String BGMcueName)
        {
            if (((_AudMan != null) || (_AudMan._audioEngine != null) || (_AudMan._aSoundBank != null) ||
                (_AudMan._bgmWaveBank != null)) /*&& !_AudMan._aSoundBank.IsInUse*/)
            {
                if (DebugFlag)
                    Console.WriteLine("[AudioManager]Playing BGM: " + BGMcueName);
                _AudMan._aSoundBank.PlayCue(BGMcueName);
            }
        }

        public static void Play_BGMusic(String MusicCueName)
        {
            if ((_AudMan == null) || (_AudMan._audioEngine == null) || (_AudMan._aSoundBank == null) ||
                (_AudMan._bgmWaveBank == null))
                return;

            if (_AudMan._bgmMusicCue != null)
                _AudMan._bgmMusicCue.Stop(AudioStopOptions.Immediate);

            _AudMan._bgmMusicCue = Get_BGMcue(MusicCueName);

            if (_AudMan._bgmMusicCue != null)
            {
                if (DebugFlag)
                    Console.WriteLine("[AudioManager]Playing BGM_m: " + MusicCueName);
                _AudMan._bgmMusicCue.Play();
            }
        }

        public static void StopAll_Music()
        {
            if ((_AudMan == null) || (_AudMan._audioEngine == null) || (_AudMan._aSoundBank == null) ||
                (_AudMan._bgmWaveBank == null) || (_AudMan._seWaveBank == null))
                return;

            if (_AudMan._bgmMusicCue != null)            
                _AudMan._bgmMusicCue.Stop(AudioStopOptions.Immediate);

            if( _AudMan._seMusicCue != null)
                _AudMan._seMusicCue.Stop(AudioStopOptions.Immediate);
            
            if (DebugFlag)
                Console.WriteLine("[AudioManager]All Audio Forcibly Stopped.");
        }

        public static void setSEVolume(float value)
        {
            value = MathHelper.Clamp(value, 0.1f, 10.0f);

            if (_AudMan != null)
                _AudMan._seVolume = value;
            
            if (DebugFlag)
                Console.WriteLine("[AudioManager]Setting SEVolume to "+ value.ToString());
        }

        public static void setBGMVolume(float value)
        {
            value = MathHelper.Clamp(value, 0.1f, 10.0f);
            if (_AudMan != null)
                _AudMan._bgmVolume = value;
            
            if (DebugFlag)
                Console.WriteLine("[AudioManager]Setting BGMVolume to "+ value.ToString());
        }

        public static void NullAudioEngine()
        {
            _AudMan._bgmMusicCue = null;
            _AudMan._seMusicCue = null;

            _AudMan._audioEngine = null;
            _AudMan._aSoundBank = null;

            _AudMan._seWaveBank = null;
            _AudMan._bgmWaveBank = null;
        }

        public static void UnNullAudioEngine()
        {
            if (!_AudMan.DefaultAudioEnginePathValues)
            {
                try
                {
                    _AudMan._audioEngine = new AudioEngine(_AudMan._aEnginePath2);
                    _AudMan._aSoundBank = new SoundBank(_AudMan._audioEngine, _AudMan._aSoundBankPath2);
                    _AudMan._seWaveBank = new WaveBank(_AudMan._audioEngine, _AudMan._seWaveBankPath2);
                    _AudMan._bgmWaveBank = new WaveBank(_AudMan._audioEngine, _AudMan._bgmWaveBankPath2, 0, 16);
                }
                catch (NoAudioHardwareException)
                {
                    _AudMan._audioEngine = null;
                    _AudMan._aSoundBank = null;
                    _AudMan._seWaveBank = null;
                    _AudMan._bgmWaveBank = null;
                }
            }
            else
            {
                try
                {
                    _AudMan._audioEngine = new AudioEngine(_aEnginePath);
                    _AudMan._aSoundBank = new SoundBank(_AudMan._audioEngine, _aSoundBankPath);
                    _AudMan._seWaveBank = new WaveBank(_AudMan._audioEngine, _seWaveBankPath);
                    _AudMan._bgmWaveBank = new WaveBank(_AudMan._audioEngine, _bgmWaveBankPath, 0, 16);
                }
                catch (NoAudioHardwareException)
                {
                    _AudMan._audioEngine = null;
                    _AudMan._aSoundBank = null;
                    _AudMan._seWaveBank = null;
                    _AudMan._bgmWaveBank = null;
                }
            }

            _AudMan._seCategory = _AudMan._audioEngine.GetCategory("SE");
            _AudMan._bgmCategory = _AudMan._audioEngine.GetCategory("BGM");

            _AudMan._seCategory.SetVolume(_AudMan._seVolume);
            _AudMan._bgmCategory.SetVolume(_AudMan._bgmVolume);
        }

        

        #endregion
    }
}
