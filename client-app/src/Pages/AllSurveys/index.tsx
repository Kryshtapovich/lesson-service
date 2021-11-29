import SurveyCard from "@Components/Cards/Survey";
import SurveyQuestionsCard from "@Components/Cards/SurveyQuestions";
import LoadingWrapper from "@Components/LoadingWrapper";
import { Box, Typography } from "@mui/material";
import useStore from "@Stores";
import { observer } from "mobx-react-lite";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

import { card, pageWrapper, previewText, surveyBlock, surveyPreview } from "./style";

function AllSurveysPage() {
  const { surveyStore } = useStore();
  const { surveys, isSurveysLoading } = surveyStore;

  const [surveyId, setSurveyId] = useState<string>();

  useEffect(() => {
    surveyStore.getSurveys();
    return () => surveyStore.dispose();
  }, [surveyStore]);

  const navigate = useNavigate();
  const resultsCallback = (surveyId: string) => {
    navigate("/survey-results", {
      state: {
        surveyId
      }
    });
  };

  const deleteCallback = (surveyId: string) => {
    surveyStore.deleteSurvey(surveyId);
    setSurveyId(undefined);
  };

  return (
    <Box sx={pageWrapper}>
      <LoadingWrapper isLoading={isSurveysLoading} size={"10%"}>
        <Box sx={surveyBlock}>
          {surveys.map((s) => (
            <SurveyCard
              key={s.id}
              sx={card}
              survey={s}
              detailsCallback={() => setSurveyId(s.id)}
              closeCallback={() => surveyStore.closeSruvey(s.id)}
              deleteCallback={() => deleteCallback(s.id)}
              resultsCallback={() => resultsCallback(s.id)}
            />
          ))}
        </Box>
      </LoadingWrapper>
      {surveyId ? (
        <SurveyQuestionsCard surveyId={surveyId} />
      ) : (
        <Box sx={surveyPreview}>
          <Typography sx={previewText}>Click Details to see survey</Typography>
        </Box>
      )}
    </Box>
  );
}

export default observer(AllSurveysPage);
