# NOA Debuggerがアプリケーションに対して実行する各種処理について

NOA Debuggerがアプリケーションに対して実行する各種処理について解説します。

## 共通

### アプリケーションのビルド時に実行する処理

`DebugCategoryBase`または`NoaCustomMenuBase`を継承しているクラスの一覧が載った`link.xml`を作成します。<br>
ビルド完了後、作成した`link.xml`を削除するため差分は発生しません。

## iOS

### アプリケーションのビルド時に実行する処理

`Info.plist` に対して、以下の値を有効に設定します。

- `LSSupportsOpeningDocumentsInPlace`: アプリケーションの `Documents` フォルダを標準の **ファイル** アプリケーション上で表示します。
- `UIFileSharingEnabled`: アプリケーションの `Documents` フォルダ上のファイルをユーザが共有することができます。

### アプリケーションのランタイム上で実行する処理

`Application.persistentDataPath` がiCloudバックアップの対象外となります。

## 関連機能

- [DebugCommandについて](./DebugCommand/DebugCommand.md)
- [独自のメニューを追加する方法](./CustomMenu.md)
- [ダウンロードについて](./Download.md)
