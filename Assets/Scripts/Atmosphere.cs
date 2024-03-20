using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atmosphere : MonoBehaviour {
  [SerializeField] private int numTilesWidth;
  [SerializeField] private int numTilesHeight;
  [SerializeField] private int tileResolution;

  private float[,] tiles;
  private Color[,] pixels;

  private Texture2D atmosphereTexture;

  private void Start() {
    InitializeAtmosphericTilemap();
    InitializeAtmosphericTexture();

    Renderer atmosphereRenderer = GetComponent<Renderer>();
    atmosphereRenderer.material.mainTexture = atmosphereTexture;
  }

  private void Update() {
    UpdateAtmosphere();
    RenderAtmosphere();
  }

  private void UpdateAtmosphere() {
    tiles[0, 0] = 1f;

    for(int y = 0; y < numTilesHeight; y++) {
      for(int x = 0; x < numTilesWidth; x++) {
        UpdateTile(x, y);
      }
    }
  }

  private void UpdateTile(int tileIndexX, int tileIndexY) {
    float localAveragePressure = 0f;
    int localTilesCount = 0;


    for(int y = tileIndexY - 1; y <= tileIndexY + 1; y++) {
      for(int x = tileIndexX - 1; x <= tileIndexX + 1; x++) {
        if(IsAtmosphericTile(x, y)) {
          localAveragePressure += tiles[x, y];
          localTilesCount += 1;
        }
      }
    }

    localAveragePressure = localAveragePressure / localTilesCount;

    for(int y = tileIndexY - 1; y <= tileIndexY + 1; y++) {
      for(int x = tileIndexX - 1; x <= tileIndexX + 1; x++) {
        if(IsAtmosphericTile(x, y)) {
          tiles[x, y] = localAveragePressure;
          Debug.Log(localAveragePressure);
        }
      }
    }
  }

  private bool IsAtmosphericTile(int tileIndexX, int tileIndexY) {
    if(tileIndexX < 0) return false;
    if(tileIndexX >= numTilesWidth) return false;

    if(tileIndexY < 0) return false;
    if(tileIndexY >= numTilesHeight) return false;

    return true;
  }

  private void InitializeAtmosphericTilemap() {
    tiles = new float[numTilesWidth, numTilesHeight];
    for(int y = 0; y < numTilesHeight; y++) {
      for(int x = 0; x < numTilesWidth; x++) {
        //  tiles[x, y] = (float)(x) / (float)(numTilesWidth);
        tiles[x, y] = 0f;
      }
    }
  }

  private void InitializeAtmosphericTexture() {
    int textureResolutionX = numTilesWidth * tileResolution;
    int textureResolutionY = numTilesHeight * tileResolution;
    pixels = new Color[textureResolutionX, textureResolutionY];

    atmosphereTexture = new Texture2D(textureResolutionX, textureResolutionY);

    for(int y = 0; y < textureResolutionY; y++) {
      for(int x = 0; x < textureResolutionX; x++) {
        atmosphereTexture.SetPixel(x, y, Color.clear);
      }
    }

    atmosphereTexture.Apply();
  }

  private void RenderAtmosphere() {
    for(int y = 0; y < numTilesHeight; y++) {
      for(int x = 0; x < numTilesWidth; x++) {
        RenderTilePixels(x, y);
      }
    }
  }

  private void RenderTilePixels(int tileIndexX, int tileIndexY) {
    int startX = tileIndexX * tileResolution;
    int endX = startX + tileResolution;
    int startY = tileIndexY * tileResolution;
    int endY = startY + tileResolution;

    Color atmosphereColor = new Color(0f, 1f, 0f, tiles[tileIndexX, tileIndexY]);

    for(int y = startY; y < endY; y++) {
      for(int x = startX; x < endX; x++) {
        atmosphereTexture.SetPixel(x, y, atmosphereColor);
      }
    }

    atmosphereTexture.Apply();
  }
}