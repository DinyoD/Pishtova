export class HtmlHelper {

  // TODO Optimize method!!
  static getCodeBySubjectName(name: string): string {
    let result: string = '';
    switch (name) {
      case "Биология":
        result = "bio"
        break;
      case "Български език":
        result = "bel"
        break;
    }

    return result;
  }
}