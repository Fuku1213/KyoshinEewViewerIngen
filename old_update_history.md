# 過去のアップデート履歴

## v0.1.0(2019/09/06 03:34)

- 初回リリース

## v0.1.1(2019/09/06 06:25)

- 『開発版の更新を利用する』を外してしまうと現状だと更新が受け取れなくなるので安定版リリースまで無効化しました。

## v0.1.2(2019/09/06 06:40)

- 更新ウィンドウのテキストが正常に表示されない場合があった点を修正しました。

## v0.1.3(2019/09/06 06:58)

- 更新チェックの処理に不具合があり、正常に動作していない場合があった点を修正しました。

## v0.2.0(2019/09/06 22:00)

- 設定画面が縦に長くなりました。
- 画面描画の軽量化を行いました。
- 常に画像解析を使用するオプションを用意しました。
- こんぽ氏の協力により、観測地点のデータを組み込むことが出来ました。今まで生成されていたファイルが使用されることはありません。

## v0.2.1(2019/09/07 20:05)

- ヒープ領域の断片化を防ぎ、起動しているとメモリ使用量が増えていく問題を修正しました。
- 64bitで動作できる場合、64bitで動作できるようになりました。

## v0.3.0(2019/09/13 11:10)

- 観測点情報を更新しました。
- 地図上の座標計算を気持ち高速化しました。
- .NET Core 3.0 に移植しました。.NET4.7をインストールしていない環境でも動作します。
- 実行ファイル名が KyoshinEewViewer.Core.exe に変更になり、libs フォルダは不要になりました。
- 走時表のデータ形式を最適化し、実行ファイルに同梱するようになりました。

## v0.3.1(2019/10/01 19:50)

- 強震モニタのURL変更に対応しました。

## v0.3.2(2019/10/05 14:35)

- 使用ライブラリを少し減らしました。  
  気持ち高速化が期待できます。

## v0.3.3(2019/10/07 04:46)

- アップデートウィンドウでリンクをクリックした際に落ちてしまう不具合を修正しました。
- ボタン･チェックボックスのスタイルをテーマに合わせるようにしました。
- 初期状態のテーマを淡色に設定しました。
- メインウィンドウ下部に設定ボタンを追加しました。

## v0.4.0(2019/10/18 18:20)

- 時刻同期の設定を追加しました。
- ログ出力の設定を追加しました。  
  初期状態では無効ですがバグ報告のときに役立つと思います。
- 最終報のEEWの表記に対応してみました。

バグだらけだと思うのでバグ報告お願いします！

## v0.4.1(2019/10/26 22:15)

- 円を描画する際のアルゴリズムを一新しました。
- 最小化時や非表示時に再描画を行わないようにしました。

## v0.4.2(2019/10/27 05:20)

- Windows10 64bit版以外のサポートを打ち切りました。
- 起動時の高速化を行いました。初回起動時･バージョンアップ直後はもう少し時間がかかります。
- 実行ファイル名が KyoshinEewViewer.exe になりました。

## v0.4.3(2019/10/27 20:00)

- 起動後初期化表示のまま止まってしまうことがある現象に対しての暫定対策を行いました。

## v0.4.4(2019/10/29 06:30)

- 試験的にEEWの際に震央を描画するようにしてみました。
- OSS化を行いました。

## v0.4.5(2019/10/31 17:05)

- 通知アイコンを実装しました。デフォルトで最小化時に通知領域に格納されます。
- アイコンが新しくなりました。
- 内部サービスクラスの範囲を変更しました。

## v0.4.6(2019/11/05 05:40)

- F11を押したときにフルスクリーンにする機能を復活させました。
- スタートメニューにピン留めした際に背景色などが表示されるようになりました。

## v0.4.7(2020/01/27 18:28)

- .NET Core 3.1にアップデートしました。
- 緊急地震速報の震度表示の文字色が黒固定になっていた部分を修正しました。

## v0.5.0(2020/02/02 17:05)

- 地震情報が表示されるようになりました。
  - 毎分20秒更新で 震度速報 震源速報 震源・震度に関する情報 に対応しています。
  - 処理が甘いため更新されなかったり、あとに発表された震度速報に振り回されたりします。
  - ファイルのキャッシュのため XmlCache というフォルダが生成されます。一定以上ファイルが溜まると自動でクリーンアップが行われます。
- ログ出力を有効にすると不具合調査に役立ちますのでご協力お願いします。

## v0.5.1(2020/02/03 08:40)

- XMLの取得処理を変更しました。起動時に表示できない地震があった部分を修正しました。

## v0.5.2(2020/02/06 20:50)

- XMLの取得に失敗した場合クラッシュしてしまう問題を修正しました。
- クラッシュした際にログを出力するようにしました。

## v0.5.3(2020/02/14 01:30)

- 地震情報･緊急地震速報のデザインを微修正しました。

## v0.6.0(2020/02/21 21:50)

- 負荷を犠牲に、マップが動かせるようになりました。
- 設定画面が少し横に長くなりました。
- データを読み込めなかった場合、自動でオフセットを調整する設定を追加しました。
- 地震情報の『ごく浅い』表記に対応しました。
- 画像解析における観測点情報を更新しました。ちょっと重複が減ったと思います。

## v0.6.1(2020/02/22 00:35)

- 緊急地震速報が正常に表示できない問題を修正しました。
- 何も考えず調整していた処理を修正しました。

## v0.6.2(2020/02/24 00:25)

注:過去バージョンとの設定ファイルの互換性がありません。再度設定をお願いします。

- 設定ファイルの新装を行いました。
- 地図表示の高速化を行いました。
- テーマに合わせてマップ上の文字の色が変化するようになりました。
- メイン画面のデザインを修正しました。
- スリープ復帰に対応しました。
- XmlCacheフォルダを一時フォルダに出力するようにしました。
- 通知設定の最小化時に非表示にする設定がうまく動いていなかったのを修正しました。

## v0.6.3(2020/03/04 04:00)

- 防災情報XMLを受信する際のHTTP圧縮を有効にしました。
- マップの表示範囲固定機能･操作ロック機能などを試験的に追加しました。

## v0.6.4(2020/03/04 05:50)

- デバッグ処理が残っていた部分を削除しました。
- ソート処理を少しだけ早くしました。

## v0.6.5(2020/03/09 06:50)

- 複数のEEWの発表に仮対応を行いました。
- PLUM法が使用された際の表示に対応しました。
- 依存ライブラリを更新しました。
- マップ上で不明と表示されていた点を修正しました。

## v0.6.6(2020/03/12 19:38)

- タイムシフト機能を実装しました。
- EEWが正常に動作しない可能性があります。

## v0.6.7(2020/04/08 04:30)

- タイムシフト時に緊急地震速報の表示がおかしくなる不具合を暫定修正しました。
- ソースコードの整理を行いました。
- 観測点情報をこちら側で構築し、リポジトリでも同梱するようになりました。
- タイムシフトのスライダーが反対になりました。

## v0.6.8(2020/04/27 18:30)

- 震度アイコンの描画方法を変更しました。
- 時刻表示部分を左上に移動してみました。
- 右上の震度情報に震度アイコンを表示しないオプションを追加しました。
- 震度アイコンテーマ ビビッド における震度4が見づらかったため色を変更しました。

## v0.6.9(2020/05/23 23:20)

- 古いビルドをリリースしていたのでビルドし直しました。これによりいくつかバグが修正されているはずです。
- タイムシフト時にEEWの表示がおかしくなる可能性があった点を修正しました。
- 日本標準時以外の環境から利用した際に正常に動かなくなる可能性を対策しました。

## v0.6.10(2020/07/16 03:10)

- ＊マップに関する複数の改善点が含まれています＊
  - EEWの円がアニメーションするようになりました！重い･1秒毎にガクガクするなどあれば報告お願いします。
  - 世界地図を同梱するようにしました。今後の機能追加にご期待ください！
  - マップ描画の軽量化を行いました。
  - たまに隙間に穴があいてしまう問題を修正しました。
  - 地図の移動できる範囲を制限しました。
  - マウスを素早く動かした際に移動できないことがある問題を修正しました。
- 著作権表示を設定画面に移動しました。
- タイムシフト時にEEWの円が残ったままになる問題を修正しました。
- 突然エラー落ちする可能性があった箇所を修正しました。
- 時刻下の表記に『強震モニタ』を追加しました。
- 複数DPI環境において画面がぼやけないようになりました。
- 設定画面のデザインを変更しました。
- インターネット時刻同期が失敗した際に正常に動作なくなっていた問題を修正しました。

## v0.6.11(2020/07/30 11:45)

- 震源のない地震情報が正常に表示できない問題を修正しました。
- 遠地地震を正常に解析できないため、一旦処理を行わないようにしました。

## v0.6.12(2020/12/16 01:12)

- 防災情報XML取得時にクラッシュする不具合を修正しました。
- .NET 5にアップデートしました。
- 依存ライブラリを更新しました。

## v0.6.13(2021/01/15 19:50)

- 震度部分のフォントを変更しました。
- 最小化時にウィンドウが非表示になった場合、うまく元の状態に戻らない問題を修正しました。
- ウィンドウのスケールを変更できるようにしました。

## v0.6.14(2021/01/17 19:10)

- 画像解析モードのみになりました。  
  画像解析時にJQuakeで使用されているアルゴリズムを使用するようになりました。圧倒的感謝！
- 画像解析の観測点情報を修正しました。
- マップデータを細かくしました。重くなってしまった方はご連絡ください。

## v0.6.15(2021/01/17 21:00)

- 小さいモニタで起動すると地図の表示がおかしくなる問題を修正しました。

## v0.7.0(2021/01/25 01:25)

- 地震情報の取得について、Project DM-D.S.Sに対応しました。  
  まだまだテスト中ですので不具合などあれば報告お願いします。  
  今後のバージョンで地図上での地震情報の表示に対応する予定です。
- 設定画面のタブの形を変更しました。  
  一部設定項目の位置も変更になっています。

## v0.7.1(2021/01/25 22:00)

- 地図の描画がちょっと軽くなったかもしれません。
- 地図設定に観測点の表示設定を追加しました。割と重いので注意してください。

## v0.7.2(2021/02/01 01:30)

- 地図をミラー図法に変更しました。縦の幅の縮小による情報量の増加を期待しています。
- リアルタイムデータの震度アイコン表示時に震度1未満の観測点をモノクロにするようにしました。
- DM-D.S.Sの以下の不具合を修正しました。
  - チャージ済み金額が正しく表示できていない問題
  - APIキー変更･削除時にWebSocketから切断されない問題

## v0.7.3(2021/02/04 13:00)

- 設定ウィンドウなどがいくつも開けてしまう問題を修正しました。

## v0.7.4(2021/02/07 12:30)

- デザイン変更を行いました。今後の機能追加の準備です。
  - それに合わせてコンボボックスのスタイルも変化しました。
- 地図の外国の色が変化しました。
- 一次細分区域の区切りが増えました。これも今後の機能追加の準備です。
- DM-D.S.SのWebSocketでの電文が破損していた場合、別途取得を試みるようにしました。
- JITプロファイルを生成するようにしました。 KyoshinEewViewer.jitprofile が生成されるようになります。

## v0.7.5(2021/02/14 00:30)

- 穴あきポリゴンへの対応を行いました。
- 海外の土地の色が変更になりました。
- 特定の情報を受け取った際にクラッシュしてしまう問題を修正しました。

## v0.7.6(2021/02/14 03:10)

- 高い震度の色をうまく変換できなかった問題を修正しました。
- WebSocketで対応していない電文を解析しようとしてしまう問題を修正しました。

## v0.7.7(2021/02/14 14:50)

- 観測できなくなった観測点の描画がうまくいっていない問題を修正しました。
- 震源に関する情報をいい感じに解釈するようにしました。

## v0.8.0(2021/02/21 10:40)

- SignalNowProfessional 連携機能を追加しました。キャンセル報などにも正式対応しました。  
  SignalNowProfessional経由で受信した場合、震源･最大震度は不明状態になります。
- そのほか、パフォーマンスの改善や軽微な修正･バグ修正を行いました。

## v0.8.1(2021/02/22 22:35)

- EEW発表中は取得間隔を1秒にするオプションが機能していない問題を問題を修正しました。
- ライトテーマ時に設定画面の注意分が見づらい問題を修正しました。

## v0.8.2(2021/04/03 20:07)

- 強震モニタのEEWが遅延した際に正常にSignalNowProfessionalのEEWが表示できない問題を修正しました。
- DM-D.S.SのAPIv2に対応しました。まだ不安定な可能性があります。

## v0.8.3(2021/04/14 23:05)

- DM-D.S.SのAPIキーを更新した際にクラッシュしていた問題を修正しました。

## v0.10.0(2021/07/27 06:05)

- フレームワークを変更しました。
- 地震情報が地図上に表示できるようになりました。

## v0.10.1(2021/07/27 17:55)

- 地震情報受信時に正常に通知が出ない問題を修正しました。

## v0.10.2(2021/07/27 22:45)

- DM-D.S.Sの切断時に落ちることがあった問題を修正しました。

## v0.10.3(2021/07/28 01:30)

- 震源情報を受信した際に、震源が ごく浅い と表示されてしまう問題を暫定修正しました。  
  暫定のため再発する可能性があります。

## v0.10.4(2021/07/29 10:35)

- Windows向けにフレームレートを下げるオプションを追加しました。
- 表示中の地震情報に対して、リスト上右クリックから過去の電文をマップに表示できるようにしました。
- DM-D.S.S接続時にトークン･スコープのチェックを行うように変更しました。

## v0.10.5(2021/07/29 22:30)

今回の更新はWindowsのみに影響があります。

- Acrylicテーマを追加しました。ウィンドウが透けます。
- メインウィンドウにカスタムタイトルバーを実装しました。

## v0.10.6(2021/07/31 08:00)

- 地震情報受信時の通知を修正しました。
- 丸角クリッピングを修正しました。

## v0.10.7(2021/07/31 10:30)

- カスタムタイトルバーに不具合があったためオミットしました。
- 終了時のウィンドウの状態を保持するようにしました。

## v0.10.8(2021/08/01 21:10)

- 誤字の都合上、DM-D.S.S連携が切れます。再度認証をお願いします。
- マップ上の文字に縁がつくようになりました。
- 地震情報受信時に時刻が 分:秒 で表示されていた問題を修正しました。

## v0.10.9(2021/08/08 02:10)

- 地震情報表示時に塗りつぶす機能が有効化されました。

## v0.10.10(2021/08/21 19:10)

- 雨雲レーダー機能が追加されました。  
  が、重いので利用は推奨しません。
- 起動時地震情報が表示されている場合、最新の情報がうまく選択されない問題を修正しました。

## v0.10.11(2021/08/25 02:20)

- 雨雲レーダー機能に自動更新が追加されました。
- 気象庁のサイトに合わせてレーダー画像が偶数のタイルしか参照しないようになりました。気象庁HPの負荷軽減にご協力お願いします。

## v0.10.12(2021/08/27 04:15)

- 長時間起動でメモリリークを起こしていた件について、暫定対処を行いました。

## v0.10.13(2021/08/29 16:48)

- レーダー画像の範囲が表示されるようになりました。
- 画像周りのキャッシュ効率の改善を行いました。

## v0.10.14(2021/09/08 02:00)

- 安定性に大きな影響を与えていたため、キャッシュの保存場所が変更になりました。  
  メモリ使用量が増加していく問題が概ね解消されました。

## v0.10.15(2021/09/13 03:30)

- DM-D.S.Sのレートリミット新設に伴う対応を行いました。
- 地震情報の内部動作を変更しました。

## v0.10.16(2021/09/16 23:30)

- フレームスキップ機能をオミットしました。フレームワークの仕様が変わったためです。
- .NET 6によるビルドに切り替えました。
- 『顕著な地震の震源要素更新のお知らせ』に対応しました。
- 観測点リストの表示方法を変更できるようにしました。

## v0.10.17(2021/09/30 15:25)

- 震源情報と震度速報を合わせて表示できるようになりました。

## v0.10.18(2021/10/08 01:30)

- 大きな地震の震源が正常に表示されない問題を修正しました。ご迷惑おかけしました。

## v0.10.19(2021/10/10 02:50)

- DM-D.S.S WebSocket利用時、回線切断などの際にエラー落ちしてしまう問題を修正しました。
- macOSの配布形態を変更しました。

## v0.10.20(2021/10/10 03:34)

- DM-D.S.S 利用時、電文取得に失敗した場合気象庁防災情報XMLを利用するようにしました。

## v0.10.21(2021/10/12 01:43)

- プライバシーポリシーが変更されました。DLページにあるプライバシーポリシーもご確認ください。
- 震源情報受信後、震度速報を受信した際に震源が正しい名称ではなくなる問題を修正しました。
- Windows11での動作が不安定なため、アクリルテーマを削除しました。ご了承ください。

## v0.10.22(2021/10/13 01:50)

- 未使用の地図データを減らし、バイナリサイズの軽量化を行いました。
- 長時間起動時、キャッシュに起因する問題を修正しました。

## v0.10.23(2021/10/17 14:00)

- アップデータを実装しました。次回更新時より、更新ウィンドウのボタンから自動で更新させることが可能です。
- 防災情報XML取得に起因するクラッシュの問題を修正しました。
- キャッシュに起因するクラッシュの問題を修正しました。

## v0.10.24(2021/10/18 00:20)

- 起動したときの地震情報取得時に震源情報しかないデータを除外するようにしました。
- Quarogカラー･ウィンドウテーマを追加しました。

## v0.10.25(2021/10/20 12:00)

- いくつかの長時間起動時のクラッシュの対策を行いました。

## v0.10.26(2021/10/23 21:16)

- アセンブリの最適化機能により機能しなくなっていたため、地震情報のXML読み込み機能をオミットしました。  
  この機能は引き続き手元でビルドを行うことで利用可能です。
- いくつかのクラッシュの対策を行いました。

## v0.10.27(2021/11/03 15:00)

- SignalNow Professional の設定を読み取り設定された座標を地図上に表示できるようにしました。

## v0.10.28(2021/11/05 21:51)

- Windowsでクラッシュ時にメッセージボックスを出すようにしました。
- ライブラリのアップデートを行いました。
- いくつか安定性の向上を行いました。

## v0.10.29(2021/11/22 01:00)

- DM-D.S.S のWebSocket切断時に正常に再接続されない問題を修正しました。

## v0.10.30(2021/12/10 23:15)

- DM-D.S.S のWebSocket再接続時にクラッシュする問題を暫定修正しました。
- まれに地震情報受信時にクラッシュする問題を修正しました。

## v0.10.31(2021/12/11 18:05)

- 震度アイコンの場所を位置調整しました。
- 震度速報受信後に震源情報を受信した場合、左下のリストの表示に誤りがある問題を修正しました。

## v0.10.32(2021/12/29 02:45)

- DM-D.S.S のWebSocket同時接続数が1の場合、再接続時にクラッシュする問題修正しました。  
  この対応のため新たに socket.close の権限が必要になっています。認証が外れますので、再認証をお願いします。
- 一部地図描画部分の再設計を行いました。EEWの円がずれる問題が軽減されました。
- 再起動などでウィンドウの位置が不正な状態で記録されてしまった場合、暫定的にウィンドウの位置を復元しないようにしました。

## v0.10.34(2022/01/03 23:30)

- 強震モニタの観測点表示をリニューアルしました。
- Linux版でクラッシュする問題･アップデータが起動できない問題を修正しました。
- mac版でウィンドウを閉じた際にクラッシュする問題が報告されています。現在修正内容を検討中です。

## v0.10.37(2022/01/06 23:30)

- 配布形態の変更を行います。  
  これ以降のアップデートは一覧に表示されない可能性があります。
