// pch.cpp: файл исходного кода, соответствующий предварительно скомпилированному заголовочному файлу

#include "pch.h"

// При использовании предварительно скомпилированных заголовочных файлов необходим следующий файл исходного кода для выполнения сборки.

extern "C" _declspec(dllexport) double Create_spline(const int cnt, const int cnt_uniform, const double* points, const double* limits, const double* points_val, const double* dvs, double* res)
{

	DFTaskPtr task;
	int i = 3;
	double* scoeff = new double[(cnt - 1) * DF_PP_CUBIC];
	MKL_INT dorder[] = { 1 , 1 , 1 };

	int status = dfdNewTask1D(&task, cnt, points, DF_NON_UNIFORM_PARTITION, 1, points_val, DF_NO_HINT);
	if (status != DF_STATUS_OK)
	{
		return status - 0.1;
	}
	status = dfdEditPPSpline1D(task, DF_PP_CUBIC, DF_PP_NATURAL, DF_BC_1ST_LEFT_DER + DF_BC_1ST_RIGHT_DER, dvs, DF_NO_IC, NULL, scoeff, DF_NO_HINT);
	if (status != DF_STATUS_OK)
	{
		return status - 0.2;
	}
	status = dfdConstruct1D(task, DF_PP_SPLINE, DF_METHOD_STD);
	if (status != DF_STATUS_OK)
	{
		return status - 0.3;
	}
	status = dfdInterpolate1D(task, DF_INTERP, DF_METHOD_PP, cnt_uniform, limits, DF_UNIFORM_PARTITION, i, dorder, NULL, res, DF_NO_HINT, NULL);
	if (status != DF_STATUS_OK)
	{
		return status - 0.4;
	}
	status = dfDeleteTask(&task);
	if (status != DF_STATUS_OK)
	{
		return status - 0.5;
	}
	return status;
}