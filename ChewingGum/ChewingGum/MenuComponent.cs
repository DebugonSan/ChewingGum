using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace ChewingGum
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class MenuComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        #region�@�t�B�[���h

        /// <summary>
        /// �O���t�B�b�N
        /// </summary>
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        /// <summary>
        /// �e�N�X�`��
        /// </summary>
        private Texture2D titleTexture;

        /// <summary>
        /// �A�C�e���e�N�X�`���F��I�����
        /// </summary>
        private Texture2D menuStartTexture;
        private Texture2D menuOptionTexture;
        private Texture2D menuExitTexture;

        /// <summary>
        /// �A�C�e���e�N�X�`���F�I�����
        /// </summary>
        private Texture2D menuStartSelectedTexture;
        private Texture2D menuOptionSelectedTexture;
        private Texture2D menuExitSelectedTexture;

        /// <summary>
        /// �A�C�e���|�W�V����
        /// </summary>
        private Vector2 position1;
        private Vector2 position2;
        private Vector2 position3;

        /// <summary>
        /// �r�f�I
        /// </summary>
        /// �Ή��t�@�C����.wmv�̂�
        /// Single CBR, VC-1, DBR����
        /// ���ڂ����d�l�͉��L�Q��
        /// https://msdn.microsoft.com/ja-jp/library/dd254869.aspx
        private Video video;
        private VideoPlayer videoPlayer;
        
        //VideoPlayer.GetTexture()�̕Ԃ�l��Texture2D�Ȃ̂ŕs�v
        //private Texture2D videoTexture;

        /// <summary>
        /// ���j���[�A�C�e��
        /// </summary>
        public enum Menu
        {
            Start,
            Option,
            Exit
        }

        /// <summary>
        /// �I�����
        /// </summary>
        Menu menu = Menu.Start;
        bool selected = false;

        #endregion

        public MenuComponent(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            // InputManager������
            InputManager.Initialize();

            //���j���[�A�C�e���̍��W�Z�b�g
            InitializePosition();

            base.Initialize();
        }

        private void InitializePosition()
        {
            position1 = new Vector2(300.0f, 100.0f);
            position2 = new Vector2(300.0f, 200.0f);
            position3 = new Vector2(300.0f, 300.0f);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            // �e�N�X�`���ǂݍ���
            LoadTexture();
            LoadVideo();
        }

        private void LoadTexture()
        {
            //�^�C�g��
            titleTexture = Game.Content.Load<Texture2D>("img\\title");
                                    
            //���j���[�A�C�e��
            menuStartTexture = Game.Content.Load<Texture2D>("img\\MenuItem\\start_white");
            menuOptionTexture = Game.Content.Load<Texture2D>("img\\MenuItem\\option_white");
            menuExitTexture = Game.Content.Load<Texture2D>("img\\MenuItem\\exit_white");

            menuStartSelectedTexture = Game.Content.Load<Texture2D>("img\\MenuItem\\start_yellow");
            menuOptionSelectedTexture = Game.Content.Load<Texture2D>("img\\MenuItem\\option_yellow");
            menuExitSelectedTexture = Game.Content.Load<Texture2D>("img\\MenuItem\\exit_yellow");
        }

        private void LoadVideo()
        {
            //�r�f�I�ǂݍ���
            video = Game.Content.Load<Video>("test");

            //�v���[���[�̃C���X�^���X���쐬
            videoPlayer = new VideoPlayer();

            //���[�v����
            videoPlayer.IsLooped = true;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            //�I�����
            if (selected)
            {
                selected = false;
            }

            #region �r�f�I�Đ�

            //�����Đ����~�܂��Ă�����Đ�
            if (videoPlayer.State == MediaState.Stopped)
                videoPlayer.Play(video);

            #endregion

            #region ���͏���

            if (InputManager.IsJustKeyDown(Keys.Up) || InputManager.IsJustButtonDown(PlayerIndex.One, Buttons.LeftThumbstickUp))
            {
                if (Menu.Start < menu)
                {
                    menu--;
                }
            }
            else if (InputManager.IsJustKeyDown(Keys.Down) || InputManager.IsJustButtonDown(PlayerIndex.One, Buttons.LeftThumbstickDown))
            {
                if (menu < Menu.Exit)
                {
                    menu++;
                }
            }
            else if (InputManager.IsJustKeyDown(Keys.Enter) || InputManager.IsJustButtonDown(PlayerIndex.One, Buttons.A))
            {
                selected = true;
            }

            #endregion

            //���͎擾����эX�V
            InputManager.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            // �X�v���C�g�������݊J�n
            spriteBatch.Begin();

            #region �r�f�I�`��

            //�r�f�I���Đ�����Ă�����`��
            if (videoPlayer.State == MediaState.Playing)
                spriteBatch.Draw(videoPlayer.GetTexture(), Vector2.Zero, Color.White);

            #endregion

            //�^�C�g�����S�`��
            spriteBatch.Draw(titleTexture, new Rectangle(0, 0, 1280, 720), Color.White);

            #region ���j���[�A�C�e���`��

            switch (menu)
            {
                case Menu.Start:
                    spriteBatch.Draw(menuStartSelectedTexture, position1, Color.White);
                    spriteBatch.Draw(menuOptionTexture, position2, Color.White);
                    spriteBatch.Draw(menuExitTexture, position3, Color.White);
                    break;

                case Menu.Option:
                    spriteBatch.Draw(menuStartTexture, position1, Color.White);
                    spriteBatch.Draw(menuOptionSelectedTexture, position2, Color.White);
                    spriteBatch.Draw(menuExitTexture, position3, Color.White);
                    break;

                case Menu.Exit:
                    spriteBatch.Draw(menuStartTexture, position1, Color.White);
                    spriteBatch.Draw(menuOptionTexture, position2, Color.White);
                    spriteBatch.Draw(menuExitSelectedTexture, position3, Color.White);
                    break;
            }

            #endregion

            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// �I�𒆂̃A�C�e��
        /// </summary>
        public Menu selectedMenu { get { return menu; } }
        public bool IsSelected()
        {
            return selected;
        }
    }
}
