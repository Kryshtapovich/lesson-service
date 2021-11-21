import { OptionModel } from "./Option";

export default interface AnswerModel {
  id: number;
  text?: string;
  option?: OptionModel;
  imageUrl?: string;
}
