export class UploadAdapter1 {
  private loader;
  editor: any;

  constructor(loader, edit) {
    this.loader = loader;
    this.editor = edit;
  }

  upload() {
    return this.loader.file.then(
      (file) =>
        new Promise((resolve, reject) => {
          var myReader = new FileReader();
          myReader.onloadend = (e) => {
            resolve({ default: myReader.result });
          };

          myReader.readAsDataURL(file);
        })
    );
  }

  data: any = `<p>Hello, world!</p>`;

  onReady(eventData) {
    eventData.plugins.get('FileRepository').createUploadAdapter = function (
      loader
    ) {
      console.log('loader : ', loader);
      console.log(btoa(loader.file));
      return new UploadAdapter(loader, this.editor);
    };
  }
}
