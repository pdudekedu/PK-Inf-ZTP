export const toChunks = <T>(array: T[], size: number) => {
  const result: T[][] = [];

  for (let i = 0; i < array.length; i += size) {
    result.push(array.slice(i, i + size));
  }

  return result;
};

export const removeElement = <T>(array: T[], element: T) => {
  const newArray = [...array];
  const index = array.indexOf(element);

  if (index !== -1) {
    newArray.splice(index, 1);
  }

  return newArray;
};

export const removeElementBy = <T>(
  array: T[],
  selector: (el: T) => boolean
) => {
  const newArray = [...array];
  const element = array.find(selector);

  if (element) {
    const index = array.indexOf(element);

    if (index !== -1) {
      newArray.splice(index, 1);
    }
  }

  return newArray;
};
