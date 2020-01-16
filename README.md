# Dummy Cursor Experiment

The README is work in progress. We re-write in English. In addition, we create a detailed document and update program.

## How to use ?  

### 練習プロセス

1. 被験者はカーソルを SPACE キーで止める．
2. 被験者は，実験者に，自分のカーソルだと思う番号を報告する．
3. 実験者は，Down Arrow キー で正解を提示する．
4. 実験者は，Right Arrow キーで次の練習セッションへ進める．

![参考画像](https://gyazo.com/18a682eeac79b51e98d22f5ab252bbb3.png "練習プロセス参考画像")

### 練習プロセス終了後，本番前

1. 実験者は，画面に "Are you ready ?" という文面が出ていることを確認する．これが出ている限り，本番は始まらない．
2. 被験者に本番の説明を行う．
3. 実験者は，被験者への説明が終わったら，Up Arrow キーを押す．
4. 実験者は， "Are you ready ?" の文面が消えたことを確認し，タイマーが動いているかを確認する．
5. タイマーが 0 を示すと，本番セッションへ突入する． Right Arrow キーを押す必要はない．

![参考画像](https://gyazo.com/b5a4f5285ba513c06636c9ea7c1890df.png "練習-本番間の参考画像")

![参考画像](https://gyazo.com/b6fac809daa075ab3f81e897085e07b6.png "タイマー起動後の画像")

### 本番プロセス

1. 被験者は，カーソルを SPACE キーで止める．
2. 被験者は，実験者に，自分のカーソルだと思う番号を報告する．
3. 実験者は，Right Arrow キーで次の練習セッションへ進める．

## Release

- 2019.06.19. ver 0.1 is released !

## 機能

- [x] 遅延
- [x] C/D 比
- [x] 発見するまでの経過時間
- [ ] 他人のカーソル再生
- [x] リアルタイム変更
- [ ] 実験モード
- [x] デバッガモード

## Requirements for development

 - git
 - Unity v2019.1.4f1

## Development

主に `/Assets/Script/` 以下を編集する．だいたいのスクリプトはSceneに配置しているGameObjectにアタッチして使う．

`/Assets/Scripts/Public` の中に全体で共有する変数や関数が入れてある．