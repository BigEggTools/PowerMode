import { PowermodePage } from './app.po';

describe('powermode App', () => {
  let page: PowermodePage;

  beforeEach(() => {
    page = new PowermodePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
