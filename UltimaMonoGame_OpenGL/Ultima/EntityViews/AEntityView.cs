﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UltimaXNA.Core.Graphics;
using UltimaXNA.Ultima.Entities;
using UltimaXNA.Ultima.Entities.Mobiles;
using UltimaXNA.Ultima.World.Controllers;
using UltimaXNA.Ultima.World.Maps;
using UltimaXNA.Ultima.World.Views;

namespace UltimaXNA.Ultima.EntityViews
{
    /// <summary>
    /// An abstract class that can be attached to an entity and used to maintain data for a 'View'.
    /// </summary>
    public abstract class AEntityView
    {
        private readonly AEntity _entity = null;
        public AEntity Entity
        {
            get { return _entity; }
        }

        public AEntityView(AEntity entity)
        {
            _entity = entity;
            SortZ = Entity.Z;
        }

        public PickType PickType = PickType.PickNothing;

        // ======================================================================
        // Sort routines
        // ======================================================================

        public int SortZ = 0;

        // ======================================================================
        // Draw methods and properties
        // ======================================================================

        public float Rotation = 0f;
        public static float PI = (float)Math.PI;

        protected bool DrawFlip = false;
        protected Rectangle DrawArea = Rectangle.Empty;
        protected Texture2D DrawTexture = null;

        public virtual bool Draw(SpriteBatch3D spriteBatch, Vector3 drawPosition, MouseOverList mouseOverList, Map map)
        {
            VertexPositionNormalTextureHue[] vertexBuffer;
            const int tolerance = 0;
            if (Math.Abs(Rotation) > tolerance)
            {
                Vector3 center = drawPosition - new Vector3(DrawArea.X - 44 + DrawArea.Width / 2, DrawArea.Y + DrawArea.Height / 2, 0);
                float sinx = (float)Math.Sin(Rotation) * DrawArea.Width / 2f;
                float cosx = (float)Math.Cos(Rotation) * DrawArea.Width / 2f;
                float siny = (float)Math.Sin(Rotation) * -DrawArea.Height / 2f;
                float cosy = (float)Math.Cos(Rotation) * -DrawArea.Height / 2f;
                // 2   0    
                // |\  |     
                // |  \|     
                // 3   1
                vertexBuffer = VertexPositionNormalTextureHue.PolyBufferFlipped;

                vertexBuffer[0].Position = center;
                vertexBuffer[0].Position.X += cosx - -siny;
                vertexBuffer[0].Position.Y -= sinx + -cosy;

                vertexBuffer[1].Position = center;
                vertexBuffer[1].Position.X += cosx - siny;
                vertexBuffer[1].Position.Y += -sinx + -cosy;

                vertexBuffer[2].Position = center;
                vertexBuffer[2].Position.X += -cosx - -siny;
                vertexBuffer[2].Position.Y += sinx + cosy;

                vertexBuffer[3].Position = center;
                vertexBuffer[3].Position.X += -cosx - siny;
                vertexBuffer[3].Position.Y += sinx + -cosy;
            }
            else
            {
                if (DrawFlip)
                {
                    // 2   0    
                    // |\  |     
                    // |  \|     
                    // 3   1
                    vertexBuffer = VertexPositionNormalTextureHue.PolyBufferFlipped;
                    vertexBuffer[0].Position = drawPosition;
                    vertexBuffer[0].Position.X += DrawArea.X + 44;
                    vertexBuffer[0].Position.Y -= DrawArea.Y;

                    vertexBuffer[1].Position = vertexBuffer[0].Position;
                    vertexBuffer[1].Position.Y += DrawArea.Height;

                    vertexBuffer[2].Position = vertexBuffer[0].Position;
                    vertexBuffer[2].Position.X -= DrawArea.Width;

                    vertexBuffer[3].Position = vertexBuffer[1].Position;
                    vertexBuffer[3].Position.X -= DrawArea.Width;
                }
                else
                {
                    // 0---1    
                    //    /     
                    //  /       
                    // 2---3
                    vertexBuffer = VertexPositionNormalTextureHue.PolyBuffer;
                    vertexBuffer[0].Position = drawPosition;
                    vertexBuffer[0].Position.X -= DrawArea.X;
                    vertexBuffer[0].Position.Y -= DrawArea.Y;

                    vertexBuffer[1].Position = vertexBuffer[0].Position;
                    vertexBuffer[1].Position.X += DrawArea.Width;

                    vertexBuffer[2].Position = vertexBuffer[0].Position;
                    vertexBuffer[2].Position.Y += DrawArea.Height;

                    vertexBuffer[3].Position = vertexBuffer[1].Position;
                    vertexBuffer[3].Position.Y += DrawArea.Height;
                }
            }

            if (vertexBuffer[0].Hue != HueVector)
                vertexBuffer[0].Hue = vertexBuffer[1].Hue = vertexBuffer[2].Hue = vertexBuffer[3].Hue = HueVector;

            if (!spriteBatch.Draw(DrawTexture, vertexBuffer))
            {
                return false;
            }

            Pick(mouseOverList, vertexBuffer);

            return true;
        }

        /// <summary>
        /// Draws all overheads, starting at [yOffset] pixels above the Entity's anchor point on the ground.
        /// </summary>
        /// <param name="yOffset"></param>
        public void DrawOverheads(SpriteBatch3D spriteBatch, Vector3 drawPosition, MouseOverList mouseOverList, Map map, int yOffset)
        {
            foreach (var tile in Entity.Overheads)
            {
                var view = tile.GetView();
                view.DrawArea = new Rectangle((view.DrawTexture.Width / 2) - 22, yOffset + view.DrawTexture.Height, view.DrawTexture.Width, view.DrawTexture.Height);
                OverheadRenderer.AddView(view, drawPosition);
                yOffset += view.DrawTexture.Height;
            }
        }

        protected void Pick(MouseOverList mouseOverList, VertexPositionNormalTextureHue[] vertexBuffer)
        {
            if ((mouseOverList.PickType & PickType) != PickType) return;
            if (((DrawFlip) || !mouseOverList.IsMouseInObject(vertexBuffer[0].Position, vertexBuffer[3].Position)) &&
                ((!DrawFlip) || !mouseOverList.IsMouseInObject(vertexBuffer[2].Position, vertexBuffer[1].Position)))
                return;
            MouseOverItem item;
            if (!DrawFlip)
            {
                item = new MouseOverItem(DrawTexture, vertexBuffer[0].Position, Entity)
                {
                    Vertices =
                        new Vector3[4]
                        {
                            vertexBuffer[0].Position, vertexBuffer[1].Position, vertexBuffer[2].Position,
                            vertexBuffer[3].Position
                        }
                };
            }
            else
            {
                item = new MouseOverItem(DrawTexture, vertexBuffer[2].Position, Entity)
                {
                    Vertices =
                        new Vector3[4]
                        {
                            vertexBuffer[2].Position, vertexBuffer[0].Position, vertexBuffer[3].Position,
                            vertexBuffer[1].Position
                        }
                };
            }
            mouseOverList.Add2DItem(item);
        }

        protected Vector2 HueVector = Vector2.Zero;

        // ======================================================================
        // Deferred drawing code
        // ======================================================================

        protected bool AllowDefer = false;

        public void SetAllowDefer()
        {
            AllowDefer = true;
        }

        protected bool CheckDefer(Map map, Vector3 drawPosition)
        {
            MapTile deferToTile;
            Direction checkDirection;
            var entity = Entity as Mobile;
            if (entity != null && entity.IsMoving)
            {
                var mobile = entity;
                switch ((mobile.Facing & Direction.FacingMask))
                {
                    case Direction.Left:
                    case Direction.South:
                    case Direction.East:
                        deferToTile = map.GetMapTile(entity.Position.X, entity.Position.Y + 1);
                        checkDirection = mobile.Facing & Direction.FacingMask;
                        break;
                    case Direction.Down:
                        deferToTile = map.GetMapTile(entity.Position.X + 1, entity.Position.Y + 1);
                        checkDirection = Direction.Down;
                        break;
                    default:
                        deferToTile = map.GetMapTile(entity.Position.X + 1, entity.Position.Y);
                        checkDirection = Direction.East;
                        break;
                }
            }
            else
            {
                deferToTile = map.GetMapTile(Entity.Position.X + 1, Entity.Position.Y);
                checkDirection = Direction.East;
            }

            if (deferToTile == null) return false;
            var mobile1 = Entity as Mobile;
            if (mobile1 != null)
            {
                var mobile = mobile1;
                int z;
                if (!MobileMovementCheck.CheckMovementForced(mobile, mobile1.Position, checkDirection, out z))
                    return false;
                var deferred = new DeferredEntity(mobile, drawPosition, z);
                deferToTile.OnEnter(deferred);
                return true;
            }
            else
            {
                var deferred = new DeferredEntity(Entity, drawPosition, Entity.Z);
                deferToTile.OnEnter(deferred);
                return true;
            }
        }
    }
}
